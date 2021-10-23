namespace CourseWork.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using LS.Helpers.Hosting.API;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    /// <inheritdoc />
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
        /// Log-in via Google.
        /// </summary>
        /// <returns>
        /// IActionResult
        /// </returns>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Server error.</response>
        [HttpPost]
        [SwaggerOperation("Log-in via Google")]
        [Produces("application/json", "application/xml")]
        [Route("api/auth/google")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> LoginViaGoogle()
        {
            return Ok();
        }
    }
}