using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Database;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Commands.Admin.DeleteBoard
{
    /// <summary>
    /// DeleteBoardCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{DeleteBoardCommand}" />
    public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, ExecutionResult>
    {
        private readonly ILogger<DeleteBoardCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteBoardCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public DeleteBoardCommandHandler(
            ILogger<DeleteBoardCommandHandler> logger,
            BaseDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: DeleteBoardCommand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult> Handle(DeleteBoardCommand request,
            CancellationToken cancellationToken)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var board = await _dbContext
                        .Boards
                        .Include(e => e.Threads)
                        .ThenInclude(e => e.Replies)
                        .ThenInclude(e => e.ReplyReplies)
                        .SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

                    if (board is null)
                    {
                        return new ExecutionResult(new ErrorInfo("No board found."));
                    }

                    var replyRepliesToDelete = board
                        .Threads
                        .SelectMany(e => e.Replies
                            .SelectMany(r => r.ReplyReplies))
                        .ToList();

                    _dbContext.ReplyReplies.RemoveRange(replyRepliesToDelete);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    _dbContext.Boards.Remove(board);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    return new ExecutionResult(new InfoMessage("The board has been deleted successfully."));
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError(e.Message);
                    return new ExecutionResult(
                        new ErrorInfo($"Error while executing {nameof(DeleteBoardCommandHandler)}"));
                }
            }
        }
    }
}