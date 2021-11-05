﻿namespace CourseWork.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core.Commands.Profile.ChangeAvatar;
    using Core.Queries.Profile.GetProfileInfo;
    using LS.Helpers.Hosting.API;
    using LS.Helpers.Hosting.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    /// <inheritdoc />
    [SwaggerTag("Profile")]
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    public sealed class ProfileController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <exception cref="ArgumentNullException">mediator</exception>
        public ProfileController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Change profile picture.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>IActionResult.</returns>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Server error.</response>
        [HttpPut]
        [SwaggerOperation("Change profile picture")]
        [Produces("application/json", "application/xml")]
        [Route("profile/avatar")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> ChangeProfilePicture([FromForm] ChangeAvatarCommand request)
        {
            var requestResult = await _mediator.Send(request);

            return this.FromExecutionResult(requestResult);
        }

        /// <summary>
        /// Get user profile info.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>IActionResult.</returns>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Server error.</response>
        [HttpGet]
        [SwaggerOperation("Get profile info")]
        [Produces("application/json", "application/xml")]
        [Route("profile")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> GetProfileInfo()
        {
            var query = new GetProfileInfoQuery();
            var requestResult = await _mediator.Send(query);

            return this.FromExecutionResult(requestResult);
        }
    }
}