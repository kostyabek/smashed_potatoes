using CourseWork.Application.Pagination;
using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Queries.Thread.GetRepliesForThread
{
    /// <summary>
    /// GetRepliesForThreadQuery.
    /// </summary>
    /// <seealso cref="T:CQRS.IQuery`1" />
    public sealed class GetRepliesForThreadQuery : PaginatedQuery, IQuery<ExecutionResult<GetRepliesForThreadQueryResult>>
    {
        /// <summary>
        /// Gets or sets the thread identifier.
        /// </summary>
        /// <value>
        /// The thread identifier.
        /// </value>
        public int ThreadId { get; set; }
    }
}