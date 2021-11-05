namespace CourseWork.Core.Commands.Profile.ChangeAvatar
{
    using CQRS;
    using LS.Helpers.Hosting.API;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// ChangeAvatarCommand
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class ChangeAvatarCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        /// <value>
        /// The avatar.
        /// </value>
        public IFormFile Avatar { get; set; }
    }
}