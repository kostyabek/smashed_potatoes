namespace CourseWork.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core.Commands.Admin.BanUser;
    using Core.Commands.Admin.RemoveBanFromUser;
    using Core.Commands.Board.CreateNewBoard;
    using Core.Models.Admin;
    using LS.Helpers.Hosting.API;
    using LS.Helpers.Hosting.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    /// <inheritdoc />
    [SwaggerTag("Admin")]
    [Authorize(Roles = "Admin")]
    [ApiExplorerSettings(GroupName = "v1")]
    public sealed class AdminController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <exception cref="ArgumentNullException">mediator</exception>
        public AdminController(IMediator mediator)
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
        [Route("admin/boards")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> CreateNewBoard([FromBody] CreateNewBoardCommand request)
        {
            var result = await _mediator.Send(request);

            return this.FromExecutionResult(result);
        }

        /// <summary>
        /// Bans the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [HttpPatch]
        [SwaggerOperation("Ban user.")]
        [Produces("application/json", "application/xml")]
        [Route("admin/users/{id:int}/ban-status")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> RemoveBanFromOrBanUser(
            [FromRoute] int id,
            [FromBody] UserBanDataModel data)
        {
            if (data.IsBanned)
            {
                var banCommand = new BanUserCommand
                {
                    UserId = id,
                    BannedUntil = data.BannedUntil,
                    IsBanPermanent = data.IsBanPermanent,
                    Reason = data.Reason
                };

                var banResult = await _mediator.Send(banCommand);

                return this.FromExecutionResult(banResult);
            }

            var removeBanCommand = new RemoveBanFromUserCommand
            {
                UserId = id,
            };

            var removeBanResult = await _mediator.Send(removeBanCommand);

            return this.FromExecutionResult(removeBanResult);
        }
    }
}