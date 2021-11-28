namespace CourseWork.Core.Models.Reply
{
    using System;
    using System.Collections.Generic;
    using User;

    /// <summary>
    /// Reply List Model.
    /// </summary>
    public class ReplyListModel : UserInfoForListModels
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime Created { get; set; }

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
        /// Gets or sets the replied to ids.
        /// </summary>
        /// <value>
        /// The replied to ids.
        /// </value>
        public List<int> RepliedToIds { get; set; }
            = new ();
    }
}
