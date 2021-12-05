namespace CourseWork.Core.Commands.Admin.DeleteBoard
{
    using CQRS;
    using LS.Helpers.Hosting.API;

    /// <summary>
    /// DeleteBoardCommand
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class DeleteBoardCommand : ICommand<ExecutionResult>
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