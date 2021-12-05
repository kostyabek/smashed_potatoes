using System;
using System.Collections.Generic;
using CourseWork.Core.Models.Reply;
using CourseWork.Core.Models.User;

namespace CourseWork.Core.Models.Thread
{
    /// <summary>
    /// Thread List Model.
    /// </summary>
    public class ThreadListModel : UserInfoForListModels
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the main picture path.
        /// </summary>
        /// <value>
        /// The main picture path.
        /// </value>
        public string MainPicturePath { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the thread starter identifier.
        /// </summary>
        /// <value>
        /// The thread starter identifier.
        /// </value>
        public int? ThreadStarterId { get; set; }

        /// <summary>
        /// Gets or sets the reply models.
        /// </summary>
        /// <value>
        /// The reply models.
        /// </value>
        public List<ReplyListModel> ReplyModels { get; set; }
            = new ();
    }
}
