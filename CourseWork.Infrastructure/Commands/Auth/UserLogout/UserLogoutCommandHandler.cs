using System;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Database.Entities.Identity;
using CourseWork.Core.Services.UserService;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Commands.Auth.UserLogout
{
    /// <summary>
    /// UserLogoutCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{UserLogoutCommand}" />
    public class UserLogoutCommandHandler : IRequestHandler<UserLogoutCommand, ExecutionResult>
    {
        private readonly ILogger<UserLogoutCommandHandler> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLogoutCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="userService">The user service.</param>
        public UserLogoutCommandHandler(
            ILogger<UserLogoutCommandHandler> logger,
            SignInManager<AppUser> signInManager,
            IUserService userService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userService = userService;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: UserLogoutCommand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult> Handle(UserLogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();

                if (user is null)
                {
                    return new ExecutionResult(new ErrorInfo("The user is not authorized!"));
                }

                await _signInManager.SignOutAsync();

                return new ExecutionResult(new InfoMessage("Signed out successfully."));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult(
                    new ErrorInfo($"Error while executing {nameof(UserLogoutCommandHandler)}"));
            }
        }
    }
}