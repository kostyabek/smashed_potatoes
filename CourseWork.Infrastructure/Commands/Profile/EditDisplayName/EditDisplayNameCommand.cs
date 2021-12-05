namespace CourseWork.Core.Commands.Profile.EditDisplayName
{
    using CQRS;
    using LS.Helpers.Hosting.API;

    /// <summary>
    /// EditDisplayNameCommand.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class EditDisplayNameCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }
    }
}