using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork.Core.Commands.Auth.UserSignIn;
using CourseWork.Core.Commands.Auth.UserSignUp;
using LS.Helpers.Hosting.API;
using LS.Helpers.Hosting.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CourseWork.Web.Controllers
{
    using Core.Commands.Auth.UserLogout;

    /// <inheritdoc />
    [ApiController]
    [SwaggerTag("Auth")]
    [ApiExplorerSettings(GroupName = "v1")]
    public sealed class AuthController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <exception cref="ArgumentNullException">mediator.</exception>
        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Sign up.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("Sign up")]
        [Produces("application/json", "application/xml")]
        [Route("sign-up")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> SignUp([FromForm] UserSignUpCommand command)
        {
            var result = await _mediator.Send(command);

            return this.FromExecutionResult(result);
        }

        /// <summary>
        /// Sign in.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("Sign in")]
        [Produces("application/json", "application/xml")]
        [Route("sign-in")]
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