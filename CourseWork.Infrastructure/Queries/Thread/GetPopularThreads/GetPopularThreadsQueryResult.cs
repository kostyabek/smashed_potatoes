namespace CourseWork.Core.Queries.Thread.GetPopularThreads
{
    using System.Collections.Generic;
    using Models.Thread;

    /// <summary>
    /// GetPopularThreadsQuery result.
    /// </summary>
    public sealed class GetPopularThreadsQueryResult
    {
        /// <summary>
        /// Gets or sets the models.
        /// </summary>
        /// <value>
        /// The models.
        /// </value>
        public List<PopularThreadModel> Models { get; set; }
    }
}