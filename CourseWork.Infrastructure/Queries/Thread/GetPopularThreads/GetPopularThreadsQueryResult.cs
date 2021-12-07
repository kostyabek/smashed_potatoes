using System.Collections.Generic;
using CourseWork.Core.Models.Thread;

namespace CourseWork.Core.Queries.Thread.GetPopularThreads
{
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