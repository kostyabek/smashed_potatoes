namespace CourseWork.Core.Database.Entities.Admin
{
    using System;
    using Identity;

    /// <summary>
    /// Ban record.
    /// </summary>
    /// <seealso cref="BaseIdentityEntity" />
    public class Ban : BaseIdentityEntity
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public AppUser User { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is permanent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is permanent; otherwise, <c>false</c>.
        /// </value>
        public bool IsPermanent { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the until.
        /// </summary>
        /// <value>
        /// The until.
        /// </value>
        public DateTime? Until { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
    }
}
