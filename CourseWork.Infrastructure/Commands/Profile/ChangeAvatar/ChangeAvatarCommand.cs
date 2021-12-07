using CourseWork.Core.Attributes;
using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;
using Microsoft.AspNetCore.Http;

namespace CourseWork.Core.Commands.Profile.ChangeAvatar
{
    /// <summary>
    /// ChangeAvatarCommand.
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