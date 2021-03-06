using System.ComponentModel.DataAnnotations;
using CourseWork.Core.Attributes;
using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CourseWork.Core.Commands.Auth.UserSignUp
{
    /// <summary>
    /// User Sign Up Command.
    /// </summary>
    /// <seealso cref="ICommand{T}" />
    /// <inheritdoc />
    public sealed class UserSignUpCommand : IRequest<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }

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