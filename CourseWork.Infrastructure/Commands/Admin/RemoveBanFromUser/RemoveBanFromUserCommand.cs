namespace CourseWork.Core.Commands.Admin.RemoveBanFromUser
{
    using CQRS;
    using LS.Helpers.Hosting.API;

    /// <summary>
    /// RemoveBanFromUserCommand.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class RemoveBanFromUserCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
    }
}