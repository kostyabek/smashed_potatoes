namespace CourseWork.Core.Commands.Auth.ResendUserEmailConfirmationLink
{
    using CQRS;
    using LS.Helpers.Hosting.API;

    /// <summary>
    /// ResendUserEmailConfirmationLinkCommand.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class ResendUserEmailConfirmationLinkCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
    }
}