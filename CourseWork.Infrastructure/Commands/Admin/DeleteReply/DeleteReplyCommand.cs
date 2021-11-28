namespace CourseWork.Core.Commands.Admin.DeleteReply
{
    using CQRS;
    using LS.Helpers.Hosting.API;

    /// <summary>
    /// DeleteReplyCommand
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