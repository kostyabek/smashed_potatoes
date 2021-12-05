using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Common.Consts;
using CourseWork.Core.Database;
using CourseWork.Core.Helpers;
using CourseWork.Core.Models.Reply;
using JetBrains.Annotations;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Queries.Thread.GetRepliesForThread
{
    /// <summary>
    /// GetRepliesForThreadQuery handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{GetRepliesForThreadQuery}" />
    [UsedImplicitly]
    public class GetRepliesForThreadQueryHandler : IRequestHandler<GetRepliesForThreadQuery,
        ExecutionResult<GetRepliesForThreadQueryResult>>
    {
        private readonly ILogger<GetRepliesForThreadQueryHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRepliesForThreadQueryHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public GetRepliesForThreadQueryHandler(
            ILogger<GetRepliesForThreadQueryHandler> logger,
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
        /// <param name="request">The request: GetRepliesForThreadQuery.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult<GetRepliesForThreadQueryResult>> Handle(
            GetRepliesForThreadQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var httpRequest = _httpContextAccessor.HttpContext.Request;
                var imagesPathBuilder = StoragePathsHelper.GetImagesStaticFilesPath(httpRequest);
                var imagesPath = imagesPathBuilder.ToString();

                var offset = request.PageSize * (request.PageNumber - 1);
                var limit = request.PageSize;

                var query = _dbContext
                    .Replies
                    .AsNoTracking()
                    .Where(e => e.ThreadId == request.ThreadId);

                var models = await query
                    .Skip(offset)
                    .Take(limit)
                    .Select(e => new ReplyListModel
                    {
                        Id = e.Id,
                        UserId = e.UserId,
                        Content = e.Content,
                        Created = e.Created,
                        UserDisplayName = e.User.DisplayName,
                        UserAvatarPath = e.User.Avatar == null ? null : $"{imagesPath}{AppConsts.StoragePaths.Avatars}/{e.User.Avatar.FileName}",
                        PicRelatedPath = e.PicRelated == null ? null : $"{imagesPath}{AppConsts.StoragePaths.RelatedPics}/{e.PicRelated.FileName}",
                        RepliedToIds = e
                                .ReplyReplies
                                .Select(rr => rr.PointedReplyId)
                                .ToList(),
                    })
                    .ToListAsync(cancellationToken);

                var totalCount = await query.CountAsync(cancellationToken);

                var result = new GetRepliesForThreadQueryResult
                {
                    Models = models,
                    TotalCount = totalCount
                };

                return new ExecutionResult<GetRepliesForThreadQueryResult>(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult<GetRepliesForThreadQueryResult>(
                    new ErrorInfo($"Error while executing {nameof(GetRepliesForThreadQuery)}"));
            }
        }
    }
}