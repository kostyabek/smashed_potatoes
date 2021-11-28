namespace CourseWork.Core.Database.Entities.Replies
{
    using Reports;

    /// <summary>
    /// Reply Report.
    /// </summary>
    public class ReplyReport : BaseIdentityEntity
    {
        /// <summary>
        /// Gets or sets the reply identifier.
        /// </summary>
        /// <value>
        /// The reply identifier.
        /// </value>
        public int ReplyId { get; set; }

        /// <summary>
        /// Gets or sets the reply.
        /// </summary>
        /// <value>
        /// The reply.
        /// </value>
        public PotatoReply Reply { get; set; }

        /// <summary>
        /// Gets or sets the report reason identifier.
        /// </summary>
        /// <value>
        /// The report reason identifier.
        /// </value>
        public int ReportReasonId { get; set; }

        /// <summary>
        /// Gets or sets the report reason.
        /// </summary>
        /// <value>
        /// The report reason.
        /// </value>
        public ReportReason ReportReason { get; set; }

        /// <summary>
        /// Gets or sets the explanation.
        /// </summary>
        /// <value>
        /// The explanation.
        /// </value>
        public string Explanation { get; set; }
    }
}
