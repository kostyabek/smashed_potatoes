namespace CourseWork.Core.Commands.Reply.ReportReply
{
    using CQRS;
    using LS.Helpers.Hosting.API;

    /// <summary>
    /// ReportReplyCommand
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class ReportReplyCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the reply identifier.
        /// </summary>
        /// <value>
        /// The reply identifier.
        /// </value>
        public int ReplyId { get; set; }

        /// <summary>
        /// Gets or sets the reason identifier.
        /// </summary>
        /// <value>
        /// The reason identifier.
        /// </value>
        public int ReasonId { get; set; }

        /// <summary>
        /// Gets or sets the explanation.
        /// </summary>
        /// <value>
        /// The explanation.
        /// </value>
        public string Explanation { get; set; }
    }
}