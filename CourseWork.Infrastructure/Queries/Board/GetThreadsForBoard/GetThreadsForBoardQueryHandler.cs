namespace CourseWork.Core.Queries.Board.GetThreadsForBoard
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Consts;
    using Database;
    using Helpers;
    using JetBrains.Annotations;
    using LS.Helpers.Hosting.API;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Models.Reply;
    using Models.Thread;

    /// <summary>
    /// GetThreadsForBoardQuery handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{GetThreadsForBoardQuery}" />
    [UsedImplicitly]
    public class GetThreadsForBoardQueryHandler : IRequestHandler<GetThreadsForBoardQuery,
        ExecutionResult<GetThreadsForBoardQueryResult>>
    {
        private readonly ILogger<GetThreadsForBoardQueryHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetThreadsForBoardQueryHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public GetThreadsForBoardQueryHandler(
            ILogger<GetThreadsForBoardQueryHandler> logger,
            BaseDbContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: GetThreadsForBoardQuery.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult<GetThreadsForBoardQueryResult>> Handle(
            GetThreadsForBoardQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var httpRequest = _httpContextAccessor.HttpContext.Request;
                var imagesPathBuilder = StoragePathsHelper.GetImagesStaticFilesPath(httpRequest);
                var imagesPath = imagesPathBuilder.ToString();

                var limit = request.PageSize;
                var offset = request.PageSize * (request.PageNumber - 1);

                var query = _dbContext
                    .Threads
                    .AsNoTracking()
                    .Where(t => t.BoardId == request.BoardId);

                var models = await query
                    .Skip(offset)
                    .Take(limit)
                    .Select(t => new ThreadListModel
                    {
                        Id = t.Id,
                        Created = t.Created,
                        Name = t.Name,
                        Description = t.Description,
                        UserId = t.UserId,
                        UserDisplayName = t.User.DisplayName,
                        UserAvatarPath = $"{imagesPath}{AppConsts.StoragePaths.Avatars}/{t.User.Avatar.FileName}",
                        MainPicturePath = $"{imagesPath}{AppConsts.StoragePaths.ThreadPics}/{t.MainPicture.FileName}",
                        ThreadStarterId = t
                            .Replies
                            .Where(r => r.IsThreadStarter)
                            .Select(r => r.Id)
                            .Single(),
                        ReplyModels = t
                            .Replies
                            .Where(r => !r.IsThreadStarter)
                            .OrderBy(r => r.Created)
                            .Take(5)
                            .Select(r => new ReplyListModel
                            {
                                Id = r.Id,
                                UserId = r.UserId,
                                Content = r.Content,
                                Created = r.Created,
                                UserDisplayName = r.User.DisplayName,
                                UserAvatarPath = r.User.Avatar == null ? null : $"{imagesPath}{AppConsts.StoragePaths.Avatars}/{r.User.Avatar.FileName}",
                                PicRelatedPath = r.PicRelated == null ? null : $"{imagesPath}{AppConsts.StoragePaths.RelatedPics}/{r.PicRelated.FileName}",
                                RepliedToIds = r
                                    .ReplyReplies
                                    .Select(rr => rr.PointedReplyId)
                                    .ToList(),
                            })
                            .ToList(),
                    })
                    .ToListAsync(cancellationToken);

                var totalCount = await query.CountAsync(cancellationToken);

                var result = new GetThreadsForBoardQueryResult
                {
                    Models = models,
                    TotalCount = totalCount,
                };

                return new ExecutionResult<GetThreadsForBoardQueryResult>(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult<GetThreadsForBoardQueryResult>(
                    new ErrorInfo($"Error while executing {nameof(GetThreadsForBoardQuery)}"));
            }
        }
    }
}