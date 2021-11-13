namespace CourseWork.Core.Queries.Thread.GetRepliesForThread
{
    using System.Collections.Generic;
    using Models.Reply;

    /// <summary>
    /// GetRepliesForThreadQuery result.
    /// </summary>
    public sealed class GetRepliesForThreadQueryResult
    {
        /// <summary>
        /// Gets or sets the models.
        /// </summary>
        /// <value>
        /// The models.
        /// </value>
        public List<ReplyListModel> Models { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalCount { get; set; }
    }
}