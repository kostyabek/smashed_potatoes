using System;
using System.Collections.Generic;
using CourseWork.Core.Database.Entities.Files;
using CourseWork.Core.Database.Entities.Replies;
using CourseWork.Core.Database.Entities.Boards;
using CourseWork.Core.Database.Entities.Identity;

namespace CourseWork.Core.Database.Entities.Threads
{
    /// <summary>
    /// Represents a thread.
    /// </summary>
    public class PotatoThread : BaseNamedEntity
    {
        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the board identifier.
        /// </summary>
        /// <value>
        /// The board identifier.
        /// </value>
        public int BoardId { get; set; }

        /// <summary>
        /// Gets or sets the board.
        /// </summary>
        /// <value>
        /// The board.
        /// </value>
        public PotatoBoard Board { get; set; }

        /// <summary>
        /// Gets or sets the replies.
        /// </summary>
        /// <value>
        /// The replies.
        /// </value>
        public List<PotatoReply> Replies { get; set; }

        /// <summary>
        /// Gets or sets the main picture identifier.
        /// </summary>
        /// <value>
        /// The main picture identifier.
        /// </value>
        public int MainPictureId { get; set; }

        /// <summary>
        /// Gets or sets the main picture.
        /// </summary>
        /// <value>
        /// The main picture.
        /// </value>
        public ImageModel MainPicture { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

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
    }
}
