namespace CourseWork.Core.Commands.Profile.ChangeAvatar
{
    using Attributes;
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
        [AllowedFiles(new[] { ".jpg", ".jpeg", ".png", ".gif" }, 10485760)]
        public IFormFile Avatar { get; set; }
    }
}