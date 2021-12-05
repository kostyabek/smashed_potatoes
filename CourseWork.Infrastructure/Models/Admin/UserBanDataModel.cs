using System;

namespace CourseWork.Core.Models.Admin
{
    /// <summary>
    /// User Ban Data Model.
    /// </summary>
    public class UserBanDataModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is banned.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is banned; otherwise, <c>false</c>.
        /// </value>
        public bool IsBanned { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ban permanent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is ban permanent; otherwise, <c>false</c>.
        /// </value>
        public bool IsBanPermanent { get; set; }

        /// <summary>
        /// Gets or sets the banned until.
        /// </summary>
        /// <value>
        /// The banned until.
        /// </value>
        public DateTime? BannedUntil { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; set; }
    }
}
