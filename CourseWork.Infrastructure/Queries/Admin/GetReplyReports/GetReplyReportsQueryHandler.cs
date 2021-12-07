using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Common.Consts;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities.Replies;
using CourseWork.Core.Helpers;
using CourseWork.Core.Models.Admin;
using JetBrains.Annotations;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Queries.Admin.GetReplyReports
{
    /// <summary>
    /// GetReplyReportsQuery handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{GetReplyReportsQuery}" />
    [UsedImplicitly]
    public class GetReplyReportsQueryHandler : IRequestHandler<GetReplyReportsQuery,
        ExecutionResult<GetReplyReportsQueryResult>>
    {
        private readonly ILogger<GetReplyReportsQueryHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetReplyReportsQueryHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public GetReplyReportsQueryHandler(
            ILogger<GetReplyReportsQueryHandler> logger,
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
        /// <param name="request">The request: GetReplyReportsQuery.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult<GetReplyReportsQueryResult>> Handle(
            GetReplyReportsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<ReplyReport, bool>> boardFilter = request.BoardId.HasValue ? e => e.Reply.Thread.BoardId == request.BoardId : e => true;
                Expression<Func<ReplyReport, bool>> dateFilter = request.Date.HasValue ? e => e.CreatedAt.Date == request.Date : e => true;

                var httpRequest = _httpContextAccessor.HttpContext.Request;
                var imagesPathBuilder = StoragePathsHelper.GetImagesStaticFilesPath(httpRequest);
                var imagesPath = imagesPathBuilder.ToString();

                var offset = request.PageSize * (request.PageNumber - 1);
                var limit = request.PageSize;

                var reports = await _dbContext
                    .ReplyReports
                    .Include(e => e.ReportReason)
                    .Include(e => e.Reporter)
                    .Include(e => e.Reply.Thread)
                    .AsNoTracking()
                    .Where(boardFilter)
                    .Where(dateFilter)
                    .Skip(offset)
                    .Take(limit)
                    .Select(e => new ReplyReportDataModel
                    {
                        ReplyId = e.ReplyId,
                        DateTime = e.CreatedAt,
                        Explanation = e.Explanation,
                        Reason = e.ReportReason.Name,
                        ReporterId = e.ReporterId,
                        ReporterAvatarPath = e.Reporter.Avatar == null ? null : $"{imagesPath}{AppConsts.StoragePaths.Avatars}/{e.Reporter.Avatar.FileName}"
                    })
                    .ToListAsync(cancellationToken);

                var result = new GetReplyReportsQueryResult { Models = reports };

                return new ExecutionResult<GetReplyReportsQueryResult>(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult<GetReplyReportsQueryResult>(
                    new ErrorInfo($"Error while executing {nameof(GetReplyReportsQuery)}"));
            }
        }
    }
}