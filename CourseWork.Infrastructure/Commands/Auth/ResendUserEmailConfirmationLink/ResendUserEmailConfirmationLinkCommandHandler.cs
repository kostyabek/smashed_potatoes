using System;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Helpers.EmailConfirmationHelper;
using CourseWork.Core.Services.UserService;
using LS.Helpers.Hosting.API;
using MediatR;
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
        private readonly IEmailConfirmationHelper _emailConfirmationHelper;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResendUserEmailConfirmationLinkCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="emailConfirmationHelper">The email confirmation helper.</param>
        /// <param name="userService">The user service.</param>
        public ResendUserEmailConfirmationLinkCommandHandler(
            ILogger<ResendUserEmailConfirmationLinkCommandHandler> logger,
            IEmailConfirmationHelper emailConfirmationHelper,
            IUserService userService)
        {
            _logger = logger;
            _emailConfirmationHelper = emailConfirmationHelper;
            _userService = userService;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: ResendUserEmailConfirmationLinkCommand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult> Handle(ResendUserEmailConfirmationLinkCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
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