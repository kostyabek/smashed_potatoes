﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.Core.Commands.Admin.BanUser;
using CourseWork.Core.Commands.Admin.DeleteBoard;
using CourseWork.Core.Commands.Admin.DeleteReply;
using CourseWork.Core.Commands.Admin.DeleteThread;
using CourseWork.Core.Commands.Admin.IgnoreReplyReports;
using CourseWork.Core.Commands.Admin.RemoveBanFromUser;
using CourseWork.Core.Commands.Board.CreateNewBoard;
using CourseWork.Core.Models.Admin;
using LS.Helpers.Hosting.API;
using LS.Helpers.Hosting.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CourseWork.Web.Controllers
{
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
        /// <exception cref="ArgumentNullException">mediator.</exception>
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
        /// Ban or remove ban from user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [HttpPatch]
        [SwaggerOperation("Ban or remove ban from user.")]
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

        /// <summary>
        /// Delete a reply.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerOperation("Delete a reply.")]
        [Produces("application/json", "application/xml")]
        [Route("admin/replies/{id:int}")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> DeleteReply([FromRoute] int id)
        {
            var request = new DeleteReplyCommand { ReplyId = id };

            var result = await _mediator.Send(request);

            return this.FromExecutionResult(result);
        }

        /// <summary>
        /// Delete a thread.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerOperation("Delete a thread.")]
        [Produces("application/json", "application/xml")]
        [Route("admin/threads/{id:int}")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> DeleteThread([FromRoute] int id)
        {
            var request = new DeleteThreadCommand { Id = id };

            var result = await _mediator.Send(request);

            return this.FromExecutionResult(result);
        }

        /// <summary>
        /// Delete a board.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerOperation("Delete a board.")]
        [Produces("application/json", "application/xml")]
        [Route("admin/boards/{id:int}")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> DeleteBoard([FromRoute] int id)
        {
            var request = new DeleteBoardCommand { Id = id };

            var result = await _mediator.Send(request);

            return this.FromExecutionResult(result);
        }

        /// <summary>
        /// Ignore reply reports.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerOperation("Ignore reply reports.")]
        [Produces("application/json", "application/xml")]
        [Route("admin/replies/{id:int}/reports")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> IgnoreReplyReport(
            [FromRoute] int id,
            [FromBody] List<int> ids)
        {
            var request = new IgnoreReplyReportsCommand
            {
                ReportIds = ids,
                ReplyId = id
            };

            var result = await _mediator.Send(request);

            return this.FromExecutionResult(result);
        }
    }
}