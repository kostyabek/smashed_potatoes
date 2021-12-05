namespace CourseWork.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Commands.Profile.ChangeAvatar;
    using Core.Commands.Profile.SetBoardsEmailSubscription;
    using Core.Queries.Profile.GetProfileInfo;
    using LS.Helpers.Hosting.API;
    using LS.Helpers.Hosting.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    /// <inheritdoc />
    [SwaggerTag("Profile")]
    [Authorize(Roles = "User,Admin,Moderator")]
    [ApiController]
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
        /// <returns>
        /// IActionResult.
        /// </returns>
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

        /// <summary>
        /// Set board email subscriptions.
        /// </summary>
        /// <param name="boardIds">The board ids.</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("Set board email subscriptions")]
        [Produces("application/json", "application/xml")]
        [Route("profile/subscription")]
        [ProducesResponseType(typeof(ExecutionResult), 200)]
        public async Task<IActionResult> SetBoardEmailSubscriptions([FromBody] List<int> boardIds)
        {
            var request = new SetBoardsEmailSubscriptionCommand { BoardIds = boardIds };
            var result = await _mediator.Send(request);

            return this.FromExecutionResult(result);
        }
    }
}