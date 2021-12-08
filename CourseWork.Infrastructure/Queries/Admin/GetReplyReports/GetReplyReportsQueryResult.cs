using System.Collections.Generic;
using CourseWork.Core.Models.Admin;

namespace CourseWork.Core.Queries.Admin.GetReplyReports
{
    /// <summary>
    /// GetReplyReportsQuery result.
    /// </summary>
    public sealed class GetReplyReportsQueryResult
    {
        /// <summary>
        /// Gets or sets the models.
        /// </summary>
        /// <value>
        /// The models.
        /// </value>
        public List<ReplyReportDataModel> Models { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalCount { get; set; }
    }
}