namespace CourseWork.Core.Queries.Board.GetThreadsForBoard
{
    using Application.Pagination;
    using CQRS;
    using LS.Helpers.Hosting.API;

    /// <summary>
    /// GetThreadsForBoardQuery.
    /// </summary>
    /// <seealso cref="T:CQRS.IQuery`1" />
    public sealed class GetThreadsForBoardQuery : PaginatedQuery, IQuery<ExecutionResult<GetThreadsForBoardQueryResult>>
    {
        /// <summary>
        /// Gets or sets the board identifier.
        /// </summary>
        /// <value>
        /// The board identifier.
        /// </value>
        public int BoardId { get; set; }
    }
}