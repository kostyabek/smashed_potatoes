namespace CourseWork.Core.Commands.Auth.UserEmailConfirmation
{
    using CQRS;
    using LS.Helpers.Hosting.API;
    using Models.Auth;

    /// <summary>
    /// UserEmailConfirmationCommand
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