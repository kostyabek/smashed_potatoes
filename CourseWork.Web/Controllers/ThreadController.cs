namespace CourseWork.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core.Commands.Thread.CreateNewThread;
    using LS.Helpers.Hosting.API;
    using LS.Helpers.Hosting.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    /// <inheritdoc />
    [SwaggerTag("Thread")]
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    public sealed class ThreadController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <exception cref="ArgumentNullException">mediator</exception>
        public ThreadController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Creates the new thread.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("Create new thread on board.")]
        [Produces("application/json", "application/xml")]
        [Route("threads")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> CreateNewThread([FromForm] CreateNewThreadCommand request)
        {
            var result = await _mediator.Send(request);

            return this.FromExecutionResult(result);
        }
    }
}