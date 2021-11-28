namespace CourseWork.Core.Commands.Reply.ReportReply
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Consts;
    using Database;
    using Database.Entities.Replies;
    using LS.Helpers.Hosting.API;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Services.UserService;

    /// <summary>
    /// ReportReplyCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{ReportReplyCommand}" />
    public class ReportReplyCommandHandler : IRequestHandler<ReportReplyCommand, ExecutionResult>
    {
        private readonly ILogger<ReportReplyCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportReplyCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userService">The user service.</param>
        public ReportReplyCommandHandler(
            ILogger<ReportReplyCommandHandler> logger,
            BaseDbContext dbContext,
            IUserService userService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userService = userService;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: ReportReplyCommand</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string</returns>
        public async Task<ExecutionResult> Handle(ReportReplyCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var userId = _userService.UserId;

                var reportModel = new ReplyReport
                {
                    ReplyId = request.ReplyId,
                    ReportReasonId = request.ReasonId,
                    CreatedAt = DateTime.UtcNow,
                    ReporterId = userId
                };

                if (reportModel.ReportReasonId == AppConsts.ReplyReportReasons.Other)
                {
                    if (string.IsNullOrWhiteSpace(request.Explanation))
                    {
                        return new ExecutionResult(new ErrorInfo("The explanation cannot be empty."));
                    }

                    reportModel.Explanation = request.Explanation;
                }

                _dbContext.ReplyReports.Add(reportModel);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new ExecutionResult(new InfoMessage("The reply has been reported successfully."));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult(
                    new ErrorInfo($"Error while executing {nameof(ReportReplyCommandHandler)}"));
            }
        }
    }
}