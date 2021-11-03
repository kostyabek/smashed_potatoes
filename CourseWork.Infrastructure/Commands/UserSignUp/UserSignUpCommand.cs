using System.ComponentModel.DataAnnotations;
using CourseWork.Common.CQRS;
using CourseWork.Core.Models.Auth;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CourseWork.Core.Commands.UserSignUp
{
    /// <summary>
    /// User Sign Up Command.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class UserSignUpCommand : IRequest<ExecutionResult<SignedInUser>>
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

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
        public IFormFile Avatar { get; set; }
    }
}