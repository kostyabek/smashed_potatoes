namespace CourseWork.Core.Queries.Profile.GetProfileInfo
{
    /// <summary>
    /// GetProfileInfoQuery result.
    /// </summary>
    public sealed class GetProfileInfoQueryResult
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the avatar path.
        /// </summary>
        /// <value>
        /// The avatar path.
        /// </value>
        public string AvatarPath { get; set; }
    }
}