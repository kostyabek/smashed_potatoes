using System;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Common.Consts;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities.Identity;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Commands.Auth.UserEmailConfirmation
{
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
        private readonly SignInManager<AppUser> _signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserEmailConfirmationCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public UserEmailConfirmationCommandHandler(
            ILogger<UserEmailConfirmationCommandHandler> logger,
            BaseDbContext dbContext,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: UserEmailConfirmationCommand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult> Handle(
            UserEmailConfirmationCommand request,
            CancellationToken cancellationToken)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var user = await _dbContext
                        .Users
                        .Include(e => e.UserRoles)
                        .SingleOrDefaultAsync(e => e.Id == request.Model.UserId, cancellationToken);

                    if (user is null)
                    {
                        return new ExecutionResult(new InfoMessage("Invalid data."));
                    }

                    var confirmationResult = await _userManager.ConfirmEmailAsync(user, request.Model.Token);

                    if (!confirmationResult.Succeeded)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return new ExecutionResult(new InfoMessage("Invalid data."));
                    }

                    user.UserRoles.Add(new AppUserRole
                    {
                        UserId = user.Id,
                        RoleId = AppConsts.UserRoles.User
                    });

                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    await _signInManager.SignOutAsync();

                    return new ExecutionResult(new InfoMessage("E-mail has been confirmed successfully."));
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError(e.Message);
                    return new ExecutionResult(
                        new ErrorInfo($"Error while executing {nameof(UserEmailConfirmationCommandHandler)}"));
                }
            }
        }
    }
}