using System;
using CourseWork.Core.Database.Entities.Identity;
using CourseWork.Core.Database.Entities.Reports;

namespace CourseWork.Core.Database.Entities.Replies
{
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
        /// Gets or sets the reporter identifier.
        /// </summary>
        /// <value>
        /// The reporter identifier.
        /// </value>
        public int ReporterId { get; set; }

        /// <summary>
        /// Gets or sets the reporter.
        /// </summary>
        /// <value>
        /// The reporter.
        /// </value>
        public AppUser Reporter { get; set; }

        /// <summary>
        /// Gets or sets the explanation.
        /// </summary>
        /// <value>
        /// The explanation.
        /// </value>
        public string Explanation { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }
    }
}
