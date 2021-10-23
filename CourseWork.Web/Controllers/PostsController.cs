// <copyright file="PostsController.cs" company="SmashedPotatoes">
// © SmashedPotatoes
// </copyright>

namespace CourseWork.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using LS.Helpers.Hosting.API;
    using LS.Helpers.Hosting.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    /// <inheritdoc />
    [SwaggerTag("Posts")]
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    public sealed class PostsController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostsController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <exception cref="ArgumentNullException">mediator</exception>
        public PostsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// CreatePostCommand.
        /// </summary>
        /// <param name="request">request</param>
        /// <returns>IActionResult</returns>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Server error.</response>
        [HttpPost]
        [SwaggerOperation("Create new post command")]
        [Produces("application/json", "application/xml")]
        [Route("api/posts")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> CreatePost()
        {
            return Ok();
        }
    }
}