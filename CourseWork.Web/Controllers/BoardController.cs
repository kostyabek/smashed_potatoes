namespace CourseWork.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core.Commands.Board.CreateNewBoard;
    using Core.Queries.Board.GetAllBoards;
    using LS.Helpers.Hosting.API;
    using LS.Helpers.Hosting.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    /// <inheritdoc />
    [SwaggerTag("Board")]
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    public sealed class BoardController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <exception cref="ArgumentNullException">mediator</exception>
        public BoardController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Create new board.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>IActionResult.</returns>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Server error.</response>
        [HttpPost]
        [SwaggerOperation("Create new board.")]
        [Produces("application/json", "application/xml")]
        [Route("boards")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> CreateNewBoard([FromBody] CreateNewBoardCommand request)
        {
            var result = await _mediator.Send(request);

            return this.FromExecutionResult(result);
        }

        /// <summary>
        /// Get all boards.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation("Get all boards.")]
        [Produces("application/json", "application/xml")]
        [Route("boards")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> GetAllBoards()
        {
            var query = new GetAllBoardsQuery();
            var result = await _mediator.Send(query);

            return this.FromExecutionResult(result);
        }
    }
}