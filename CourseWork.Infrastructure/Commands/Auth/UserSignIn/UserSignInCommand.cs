using CourseWork.Common.CQRS;
using CourseWork.Core.Models.Auth;
using LS.Helpers.Hosting.API;
using MediatR;

namespace CourseWork.Core.Commands.Auth.UserSignIn
{
    /// <summary>
    /// User Sign In Command.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class UserSignInCommand : IRequest<ExecutionResult<SignedInUser>>
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }
}