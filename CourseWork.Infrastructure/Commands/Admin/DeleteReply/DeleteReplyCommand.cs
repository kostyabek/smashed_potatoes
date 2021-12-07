using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Commands.Admin.DeleteReply
{
    /// <summary>
    /// DeleteReplyCommand.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class DeleteReplyCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the reply identifier.
        /// </summary>
        /// <value>
        /// The reply identifier.
        /// </value>
        public int ReplyId { get; set; }
    }
}