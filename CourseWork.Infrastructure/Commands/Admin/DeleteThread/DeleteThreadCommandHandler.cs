namespace CourseWork.Core.Commands.Admin.DeleteThread
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using LS.Helpers.Hosting.API;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// DeleteThreadCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{DeleteThreadCommand}" />
    public class DeleteThreadCommandHandler : IRequestHandler<DeleteThreadCommand, ExecutionResult>
    {
        private readonly ILogger<DeleteThreadCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteThreadCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public DeleteThreadCommandHandler(
            ILogger<DeleteThreadCommandHandler> logger,
            BaseDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: DeleteThreadCommand</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string</returns>
        public async Task<ExecutionResult> Handle(DeleteThreadCommand request,
            CancellationToken cancellationToken)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var thread = await _dbContext
                        .Threads
                        .SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

                    var repliesToBeDeletedManually = await _dbContext
                        .ReplyReplies
                        .Where(e => e.PointedReply.ThreadId == request.Id)
                        .ToListAsync(cancellationToken);

                    _dbContext.ReplyReplies.RemoveRange(repliesToBeDeletedManually);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    _dbContext.Threads.Remove(thread);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    return new ExecutionResult(new InfoMessage("The thread has been deleted successfully."));
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError(e.Message);
                    return new ExecutionResult(
                        new ErrorInfo($"Error while executing {nameof(DeleteThreadCommandHandler)}"));
                }
            }
        }
    }
}