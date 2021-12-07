using CourseWork.Core.CQRS;
using CourseWork.Core.Models.Auth;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Commands.Auth.UserEmailConfirmation
{
    /// <summary>
    /// UserEmailConfirmationCommand.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class UserEmailConfirmationCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public UserEmailConfirmationModel Model { get; set; }
    }
}