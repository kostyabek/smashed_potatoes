using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Commands.Admin.DeleteThread
{
    /// <summary>
    /// DeleteThreadCommand.
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