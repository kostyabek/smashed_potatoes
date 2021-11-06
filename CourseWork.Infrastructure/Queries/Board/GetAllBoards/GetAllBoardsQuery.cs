using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Queries.Board.GetAllBoards
{
    /// <inheritdoc />
    /// <summary>
    /// GetAllBoardsQuery.
    /// </summary>
    /// <seealso cref="T:CQRS.IQuery`1" />
    public sealed class GetAllBoardsQuery : IQuery<ExecutionResult<GetAllBoardsQueryResult>>
    {
    }
}