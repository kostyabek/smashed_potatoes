using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Queries.Thread.GetPopularThreads
{
    /// <inheritdoc />
    /// <summary>
    /// GetPopularThreadsQuery.
    /// </summary>
    /// <seealso cref="T:CQRS.IQuery`1" />
    public sealed class GetPopularThreadsQuery : IQuery<ExecutionResult<GetPopularThreadsQueryResult>>
    {
    }
}