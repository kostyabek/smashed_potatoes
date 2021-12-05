namespace CourseWork.Core.Commands.Auth.UserEmailConfirmation
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities.Identity;
    using LS.Helpers.Hosting.API;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// UserEmailConfirmationCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{UserEmailConfirmationCommand}" />
    public class
        UserEmailConfirmationCommandHandler : IRequestHandler<UserEmailConfirmationCommand,
            ExecutionResult>
    {
        private readonly ILogger<UserEmailConfirmationCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserEmailConfirmationCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userManager">The user manager.</param>
        public UserEmailConfirmationCommandHandler(
            ILogger<UserEmailConfirmationCommandHandler> logger,
            BaseDbContext dbContext,
            UserManager<AppUser> userManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: UserEmailConfirmationCommand</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string</returns>
        public async Task<ExecutionResult> Handle(
            UserEmailConfirmationCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var user = await _dbContext
                    .Users
                    .SingleOrDefaultAsync(e => e.Id == request.Model.UserId, cancellationToken);

                if (user is null)
                {
                    return new ExecutionResult(new InfoMessage("Invalid data."));
                }

                var confirmationResult = await _userManager.ConfirmEmailAsync(user, request.Model.Token);

                if (!confirmationResult.Succeeded)
                {
                    return new ExecutionResult(new InfoMessage("Invalid data."));
                }

                return new ExecutionResult(new InfoMessage("E-mail has been confirmed successfully."));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult(
                    new ErrorInfo($"Error while executing {nameof(UserEmailConfirmationCommandHandler)}"));
            }
        }
    }
}