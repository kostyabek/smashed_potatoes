namespace CourseWork.Core.Queries.Board.GetThreadsForBoard
{
    using System.Collections.Generic;
    using Models.Thread;

    /// <summary>
    /// GetThreadsForBoardQuery result.
    /// </summary>
    public sealed class GetThreadsForBoardQueryResult
    {
        /// <summary>
        /// Gets or sets the models.
        /// </summary>
        /// <value>
        /// The models.
        /// </value>
        public List<ThreadListModel> Models { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value> 
        /// The total count.
        /// </value>
        public long TotalCount { get; set; }
    }
}