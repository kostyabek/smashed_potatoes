namespace CourseWork.Core.Commands.Admin.DeleteThread
{
    using CQRS;
    using LS.Helpers.Hosting.API;

    /// <summary>
    /// DeleteThreadCommand
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class DeleteThreadCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
    }
}