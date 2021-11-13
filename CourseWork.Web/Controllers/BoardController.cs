using System;
using System.Threading.Tasks;
using CourseWork.Core.Commands.Board.CreateNewBoard;
using CourseWork.Core.Queries.Board.GetAllBoards;
using LS.Helpers.Hosting.API;
using LS.Helpers.Hosting.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CourseWork.Web.Controllers
{
    using Core.Queries.Board.GetThreadsForBoard;

    /// <inheritdoc />
    [SwaggerTag("Board")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    public sealed class BoardController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <exception cref="ArgumentNullException">mediator.</exception>
        public BoardController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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

        /// <summary>
        /// Gets the threads for board.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation("Gets threads for board.")]
        [Produces("application/json", "application/xml")]
        [Route("boards/{id:int}/threads")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> GetThreadsForBoard(
            [FromRoute] int id,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var query = new GetThreadsForBoardQuery
            {
                BoardId = id,
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            var result = await _mediator.Send(query);

            return this.FromExecutionResult(result);
        }
    }
}