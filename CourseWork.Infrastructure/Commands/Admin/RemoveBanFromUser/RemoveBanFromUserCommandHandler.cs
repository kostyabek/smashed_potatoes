namespace CourseWork.Core.Commands.Admin.RemoveBanFromUser
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
    /// RemoveBanFromUserCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{RemoveBanFromUserCommand}" />
    public class
        RemoveBanFromUserCommandHandler : IRequestHandler<RemoveBanFromUserCommand, ExecutionResult>
    {
        private readonly ILogger<RemoveBanFromUserCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveBanFromUserCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public RemoveBanFromUserCommandHandler(
            ILogger<RemoveBanFromUserCommandHandler> logger,
            BaseDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: RemoveBanFromUserCommand</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult> Handle(RemoveBanFromUserCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var userToRemoveBanFrom = await _dbContext
                    .Users
                    .Include(e => e.Bans)
                    .SingleOrDefaultAsync(e => e.Id == request.UserId, cancellationToken);

                if (userToRemoveBanFrom is null)
                {
                    return new ExecutionResult(new ErrorInfo("No such user has been found."));
                }

                if (!userToRemoveBanFrom.Bans.Any(e => e.IsActive))
                {
                    return new ExecutionResult(new ErrorInfo("The user has not been banned."));
                }

                if (userToRemoveBanFrom.Bans.Any(e => e.IsActive && e.IsPermanent))
                {
                    return new ExecutionResult(
                        new ErrorInfo("Cannot remove ban - the user has been banned permanently."));
                }

                var banToBeRemoved = userToRemoveBanFrom
                    .Bans
                    .SingleOrDefault(e => e.IsActive);

                banToBeRemoved.IsActive = false;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new ExecutionResult(new InfoMessage("Removed ban from user successfully."));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult(
                    new ErrorInfo($"Error while executing {nameof(RemoveBanFromUserCommandHandler)}"));
            }
        }
    }
}