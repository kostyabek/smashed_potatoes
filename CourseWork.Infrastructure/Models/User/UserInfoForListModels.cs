namespace CourseWork.Core.Models.User
{
    /// <summary>
    /// User Info For List Models.
    /// </summary>
    public class UserInfoForListModels
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user avatar path.
        /// </summary>
        /// <value>
        /// The user avatar path.
        /// </value>
        public string UserAvatarPath { get; set; }

        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        /// <value>
        /// The display name of the user.
        /// </value>
        public string UserDisplayName { get; set; }
    }
}
