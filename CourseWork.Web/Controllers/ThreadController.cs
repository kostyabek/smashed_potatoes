using System;
using System.Threading.Tasks;
using CourseWork.Core.Commands.Thread.CreateNewThread;
using CourseWork.Core.Queries.Thread.GetPopularThreads;
using LS.Helpers.Hosting.API;
using LS.Helpers.Hosting.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CourseWork.Web.Controllers
{
    /// <inheritdoc />
    [SwaggerTag("Thread")]
    [Authorize]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public sealed class ThreadController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <exception cref="ArgumentNullException">mediator.</exception>
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

        /// <summary>
        /// Gets the popular threads.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation("Gets popular threads.")]
        [Produces("application/json", "application/xml")]
        [Route("threads/popular")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> GetPopularThreads()
        {
            var query = new GetPopularThreadsQuery();
            var result = await _mediator.Send(query);

            return this.FromExecutionResult(result);
        }
    }
}