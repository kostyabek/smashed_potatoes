namespace CourseWork.Core.Queries.Thread.GetPopularThreads
{
    using CQRS;
    using LS.Helpers.Hosting.API;

    /// <inheritdoc />
    /// <summary>
    /// GetPopularThreadsQuery.
    /// </summary>
    /// <seealso cref="T:CQRS.IQuery`1" />
    public sealed class GetPopularThreadsQuery : IQuery<ExecutionResult<GetPopularThreadsQueryResult>>
    {
    }
}