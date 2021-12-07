using System;

namespace CourseWork.Core.Models.Admin
{
    /// <summary>
    /// Banned User List Item Model.
    /// </summary>
    public class BannedUserListItemModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the avatar path.
        /// </summary>
        /// <value>
        /// The avatar path.
        /// </value>
        public string AvatarPath { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the banned until.
        /// </summary>
        /// <value>
        /// The banned until.
        /// </value>
        public DateTime? BannedUntil { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ban permanent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is ban permanent; otherwise, <c>false</c>.
        /// </value>
        public bool IsBanPermanent { get; set; }
    }
}
