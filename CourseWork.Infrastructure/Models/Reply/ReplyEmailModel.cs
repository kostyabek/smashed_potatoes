namespace CourseWork.Core.Models.Reply
{
    /// <summary>
    /// Reply Email Model.
    /// </summary>
    public class ReplyEmailModel
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the pic related path.
        /// </summary>
        /// <value>
        /// The pic related path.
        /// </value>
        public string PicRelatedPath { get; set; }

        /// <summary>
        /// Gets or sets the pic related content identifier.
        /// </summary>
        /// <value>
        /// The pic related content identifier.
        /// </value>
        public string PicRelatedContentId { get; set; }

        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        /// <value>
        /// The display name of the user.
        /// </value>
        public string UserDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the user avatar path.
        /// </summary>
        /// <value>
        /// The user avatar path.
        /// </value>
        public string UserAvatarPath { get; set; }

        /// <summary>
        /// Gets or sets the user avatar content identifier.
        /// </summary>
        /// <value>
        /// The user avatar content identifier.
        /// </value>
        public string UserAvatarContentId { get; set; }
    }
}
