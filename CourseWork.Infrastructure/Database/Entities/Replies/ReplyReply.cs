namespace CourseWork.Core.Database.Entities.Replies
{
    /// <summary>
    /// Represents replies many-to-many relationship.
    /// </summary>
    public class ReplyReply
    {
        /// <summary>
        /// Gets or sets the pointing reply identifier.
        /// </summary>
        /// <value>
        /// The pointing reply identifier.
        /// </value>
        public int PointingReplyId { get; set; }

        /// <summary>
        /// Gets or sets the pointing reply.
        /// </summary>
        /// <value>
        /// The pointing reply.
        /// </value>
        public PotatoReply PointingReply { get; set; }

        /// <summary>
        /// Gets or sets the pointed reply identifier.
        /// </summary>
        /// <value>
        /// The pointed reply identifier.
        /// </value>
        public int PointedReplyId { get; set; }

        /// <summary>
        /// Gets or sets the pointed reply.
        /// </summary>
        /// <value>
        /// The pointed reply.
        /// </value>
        public PotatoReply PointedReply { get; set; }
    }
}
