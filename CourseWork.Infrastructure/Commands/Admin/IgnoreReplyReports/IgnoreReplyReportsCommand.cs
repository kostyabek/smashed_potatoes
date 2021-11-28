namespace CourseWork.Core.Commands.Admin.IgnoreReplyReport
{
    using System.Collections.Generic;
    using CQRS;
    using LS.Helpers.Hosting.API;

    /// <summary>
    /// IgnoreReplyReportCommand
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class IgnoreReplyReportsCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the reply identifier.
        /// </summary>
        /// <value>
        /// The reply identifier.
        /// </value>
        public int ReplyId { get; set; }

        /// <summary>
        /// Gets or sets the report ids.
        /// </summary>
        /// <value>
        /// The report ids.
        /// </value>
        public List<int> ReportIds { get; set; }
    }
}