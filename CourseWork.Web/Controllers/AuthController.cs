using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using LS.Helpers.Hosting.API;
using LS.Helpers.Hosting.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CourseWork.Web.Controllers
{
    using Infrastructure.Commands.UserSignUpCommand;

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
        /// <exception cref="ArgumentNullException">mediator</exception>
        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Signs up.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("Sign up with e-mail")]
        [Produces("application/json", "application/xml")]
        [Route("sign-up")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpCommand command)
        {
            var result = await _mediator.Send(command);

            return this.FromExecutionResult(result);
        }

        /// <summary>
        /// Log-in via Google.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// IActionResult.
        /// </returns>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Server error.</response>
        [HttpPost]
        [SwaggerOperation("Log-in via Google")]
        [Produces("application/json", "application/xml")]
        [Route("api/auth/google")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> LoginViaGoogle([FromBody] string username)
        {
            if (username == "kostyabek")
            {
                var claims = new List<Claim>
                {
                    new (ClaimTypes.Email, "qwer@gmail.com")
                };

                var identity = new ClaimsIdentity(claims);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                return Ok(principal);
            }

            return Unauthorized();
        }
    }
}