using System;

namespace CourseWork.Core.Models.Admin
{
    /// <summary>
    /// Reply Report Data Model.
    /// </summary>
    public class ReplyReportDataModel
    {
        /// <summary>
        /// Gets or sets the reply identifier.
        /// </summary>
        /// <value>
        /// The reply identifier.
        /// </value>
        public int ReplyId { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the explanation.
        /// </summary>
        /// <value>
        /// The explanation.
        /// </value>
        public string Explanation { get; set; }

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the reporter identifier.
        /// </summary>
        /// <value>
        /// The reporter identifier.
        /// </value>
        public int ReporterId { get; set; }

        /// <summary>
        /// Gets or sets the reporter avatar path.
        /// </summary>
        /// <value>
        /// The reporter avatar path.
        /// </value>
        public string ReporterAvatarPath { get; set; }
    }
}
