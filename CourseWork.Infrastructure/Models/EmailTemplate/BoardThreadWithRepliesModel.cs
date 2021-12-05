using System.Collections.Generic;
using CourseWork.Core.Models.Reply;

namespace CourseWork.Core.Models.EmailTemplate
{
    /// <summary>
    /// Board Thread With Replies Model.
    /// </summary>
    public class BoardThreadWithRepliesModel
    {
        /// <summary>
        /// Gets or sets the name of the board.
        /// </summary>
        /// <value>
        /// The name of the board.
        /// </value>
        public string BoardName { get; set; }

        /// <summary>
        /// Gets or sets the thread identifier.
        /// </summary>
        /// <value>
        /// The thread identifier.
        /// </value>
        public int ThreadId { get; set; }

        /// <summary>
        /// Gets or sets the name of the thread.
        /// </summary>
        /// <value>
        /// The name of the thread.
        /// </value>
        public string ThreadName { get; set; }

        /// <summary>
        /// Gets or sets the thread main picture path.
        /// </summary>
        /// <value>
        /// The thread main picture path.
        /// </value>
        public string ThreadMainPicturePath { get; set; }

        /// <summary>
        /// Gets or sets the thread main picture content identifier.
        /// </summary>
        /// <value>
        /// The thread main picture content identifier.
        /// </value>
        public string ThreadMainPictureContentId { get; set; }

        /// <summary>
        /// Gets or sets the number of replies.
        /// </summary>
        /// <value>
        /// The number of replies.
        /// </value>
        public int NumberOfReplies { get; set; }
    }
}
