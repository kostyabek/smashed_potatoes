using System;
using System.Threading.Tasks;
using CourseWork.Core.Commands.Auth.UserSignIn;
using CourseWork.Core.Commands.Auth.UserSignUp;
using LS.Helpers.Hosting.API;
using LS.Helpers.Hosting.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CourseWork.Web.Controllers
{
    using Core.Commands.Auth.ResendUserEmailConfirmationLink;
    using Core.Commands.Auth.UserEmailConfirmation;
    using Core.Commands.Auth.UserLogout;
    using Core.Database.Entities.Identity;
    using Core.Models.Auth;
    using Microsoft.AspNetCore.Identity;

    /// <inheritdoc />
    [ApiController]
    [SwaggerTag("Auth")]
    [ApiExplorerSettings(GroupName = "v1")]
    public sealed class AuthController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="userManager">The user manager.</param>
        /// <exception cref="ArgumentNullException">mediator.</exception>
        public AuthController(IMediator mediator,
            UserManager<AppUser> userManager)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userManager = userManager;
        }

        /// <summary>
        /// Sign up.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("Sign up")]
        [Produces("application/json", "application/xml")]
        [Route("auth/sign-up")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> SignUp([FromForm] UserSignUpCommand command)
        {
            var result = await _mediator.Send(command);

            return this.FromExecutionResult(result);
        }

        /// <summary>
        /// Resend the e-mail confirmation link.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("Resend e-mail confirmation link")]
        [Produces("application/json", "application/xml")]
        [Route("auth/e-mail-confirmation-link")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> ResendEmailConfirmationLink([FromBody] ResendUserEmailConfirmationLinkCommand command)
        {
            var result = await _mediator.Send(command);

            return this.FromExecutionResult(result);
        }

        /// <summary>
        /// Confirm e-mail.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation("Confirm e-mail")]
        [Produces("application/json", "application/xml")]
        [Route("auth/e-mail", Name = "ConfirmEmail")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> ConfirmProfileEmail([FromQuery] UserEmailConfirmationModel model)
        {
            var request = new UserEmailConfirmationCommand { Model = model };
            var requestResult = await _mediator.Send(request);

            return this.FromExecutionResult(requestResult);
        }

        /// <summary>
        /// Sign in.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("Sign in")]
        [Produces("application/json", "application/xml")]
        [Route("auth/sign-in")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> SignIn([FromBody] UserSignInCommand command)
        {
            var result = await _mediator.Send(command);

            return this.FromExecutionResult(result);
        }

        /// <summary>
        /// Sign out.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("Logout")]
        [Produces("application/json", "application/xml")]
        [Route("sign-out")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> LogOut()
        {
            var command = new UserLogoutCommand();
            var result = await _mediator.Send(command);

            return this.FromExecutionResult(result);
        }
    }
}