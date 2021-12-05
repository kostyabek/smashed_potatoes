using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Commands.Admin.IgnoreReplyReport;
using CourseWork.Core.Database;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Commands.Admin.IgnoreReplyReports
{
    /// <summary>
    /// IgnoreReplyReportCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{IgnoreReplyReportCommand}" />
    public class
        IgnoreReplyReportsCommandHandler : IRequestHandler<IgnoreReplyReportsCommand, ExecutionResult>
    {
        private readonly ILogger<IgnoreReplyReportsCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreReplyReportsCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public IgnoreReplyReportsCommandHandler(
            ILogger<IgnoreReplyReportsCommandHandler> logger,
            BaseDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: IgnoreReplyReportCommand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult> Handle(IgnoreReplyReportsCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var reports = await _dbContext
                    .ReplyReports
                    .Where(e => request.ReportIds.Contains(e.Id))
                    .ToListAsync(cancellationToken);

                _dbContext.ReplyReports.RemoveRange(reports);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new ExecutionResult(new InfoMessage("Reports have been successfully ignored."));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult(
                    new ErrorInfo($"Error while executing {nameof(IgnoreReplyReportsCommandHandler)}"));
            }
        }
    }
}