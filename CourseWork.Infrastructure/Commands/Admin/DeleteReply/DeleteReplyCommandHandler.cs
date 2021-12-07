using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Database;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Commands.Admin.DeleteReply
{
    /// <summary>
    /// DeleteReplyCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{DeleteReplyCommand}" />
    public class DeleteReplyCommandHandler : IRequestHandler<DeleteReplyCommand, ExecutionResult>
    {
        private readonly ILogger<DeleteReplyCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteReplyCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public DeleteReplyCommandHandler(
            ILogger<DeleteReplyCommandHandler> logger,
            BaseDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: DeleteReplyCommand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult> Handle(DeleteReplyCommand request,
            CancellationToken cancellationToken)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var reply = await _dbContext
                        .Replies
                        .SingleOrDefaultAsync(e => e.Id == request.ReplyId, cancellationToken);

                    if (reply is null)
                    {
                        return new ExecutionResult(new ErrorInfo("No such reply found."));
                    }

                    var associatedReplyReplies = await _dbContext
                        .ReplyReplies
                        .Where(e => e.PointedReplyId == request.ReplyId)
                        .ToListAsync(cancellationToken);

                    if (associatedReplyReplies.Any())
                    {
                        _dbContext.RemoveRange(associatedReplyReplies);
                        await _dbContext.SaveChangesAsync(cancellationToken);
                    }

                    _dbContext.Remove(reply);

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    return new ExecutionResult(new InfoMessage("The reply has been deleted successfully."));
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError(e.Message);
                    return new ExecutionResult(
                        new ErrorInfo($"Error while executing {nameof(DeleteReplyCommandHandler)}"));
                }
            }
        }
    }
}