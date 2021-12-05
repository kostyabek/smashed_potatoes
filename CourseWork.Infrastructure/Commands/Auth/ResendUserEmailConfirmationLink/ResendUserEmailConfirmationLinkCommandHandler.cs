using System;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Database.Entities.Identity;
using CourseWork.Core.Helpers.EmailConfirmationHelper;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Commands.Auth.ResendUserEmailConfirmationLink
{
    /// <summary>
    /// ResendUserEmailConfirmationLinkCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{ResendUserEmailConfirmationLinkCommand}" />
    public class
        ResendUserEmailConfirmationLinkCommandHandler : IRequestHandler<
            ResendUserEmailConfirmationLinkCommand, ExecutionResult>
    {
        private readonly ILogger<ResendUserEmailConfirmationLinkCommandHandler> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailConfirmationHelper _emailConfirmationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResendUserEmailConfirmationLinkCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="emailConfirmationHelper">The email confirmation helper.</param>
        public ResendUserEmailConfirmationLinkCommandHandler(
            ILogger<ResendUserEmailConfirmationLinkCommandHandler> logger,
            UserManager<AppUser> userManager,
            IEmailConfirmationHelper emailConfirmationHelper)
        {
            _logger = logger;
            _userManager = userManager;
            _emailConfirmationHelper = emailConfirmationHelper;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: ResendUserEmailConfirmationLinkCommand</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string</returns>
        public async Task<ExecutionResult> Handle(ResendUserEmailConfirmationLinkCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    return new ExecutionResult(new ErrorInfo("No such user found."));
                }

                await _emailConfirmationHelper.SendEmailConfirmationLink(user);

                return new ExecutionResult(new InfoMessage("E-mail confirmation link has been resent successfully."));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult(
                    new ErrorInfo($"Error while executing {nameof(ResendUserEmailConfirmationLinkCommandHandler)}"));
            }
        }
    }
}