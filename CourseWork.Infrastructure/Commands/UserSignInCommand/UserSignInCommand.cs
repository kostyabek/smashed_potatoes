using CourseWork.Application.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Infrastructure.Commands.UserSignInCommand
{
    /// <summary>
    /// UserSignInCommand
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class UserSignInCommand : ICommand<ExecutionResult>
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