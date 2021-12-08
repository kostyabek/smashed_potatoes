using CourseWork.Application.Pagination;
using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Queries.Board.GetAllBoards
{ 
    /// <summary>
    /// GetAllBoardsQuery.
    /// </summary>
    /// <seealso cref="T:CQRS.IQuery`1" />
    public sealed class GetAllBoardsQuery : PaginatedQuery, IQuery<ExecutionResult<GetAllBoardsQueryResult>>
    {
    }
}