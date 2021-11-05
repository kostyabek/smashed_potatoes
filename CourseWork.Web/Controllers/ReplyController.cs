namespace CourseWork.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core.Commands.Reply.CreateNewReply;
    using LS.Helpers.Hosting.API;
    using LS.Helpers.Hosting.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    /// <inheritdoc />
    [SwaggerTag("Reply")]
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    public sealed class ReplyController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplyController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <exception cref="ArgumentNullException">mediator</exception>
        public ReplyController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Creates the new reply.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("Create new reply on thread.")]
        [Produces("application/json", "application/xml")]
        [Route("replies")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> CreateNewReply([FromForm] CreateNewReplyCommand request)
        {
            var result = await _mediator.Send(request);

            return this.FromExecutionResult(result);
        }
    }
}