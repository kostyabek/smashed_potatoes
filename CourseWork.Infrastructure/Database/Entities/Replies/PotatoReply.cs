using System.Collections.Generic;
using CourseWork.Core.Database.Entities.Files;
using CourseWork.Core.Database.Entities.Identity;
using CourseWork.Core.Database.Entities.Threads;

namespace CourseWork.Core.Database.Entities.Replies
{
    /// <summary>
    /// Represents a reply in a thread.
    /// </summary>
    public class PotatoReply : BaseIdentityEntity
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
        /// Gets or sets the thread identifier.
        /// </summary>
        /// <value>
        /// The thread identifier.
        /// </value>
        public int ThreadId { get; set; }

        /// <summary>
        /// Gets or sets the thread.
        /// </summary>
        /// <value>
        /// The thread.
        /// </value>
        public PotatoThread Thread { get; set; }

        /// <summary>
        /// Gets or sets the pic related identifier.
        /// </summary>
        /// <value>
        /// The pic related identifier.
        /// </value>
        public int? PicRelatedId { get; set; }

        /// <summary>
        /// Gets or sets the pic related.
        /// </summary>
        /// <value>
        /// The pic related.
        /// </value>
        public ImageModel PicRelated { get; set; }

        /// <summary>
        /// Gets or sets the reply replies.
        /// </summary>
        /// <value>
        /// The reply replies.
        /// </value>
        public List<ReplyReply> ReplyReplies { get; set; }
    }
}
