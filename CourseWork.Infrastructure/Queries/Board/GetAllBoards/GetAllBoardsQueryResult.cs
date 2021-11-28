namespace CourseWork.Core.Queries.Board.GetAllBoards
{
    using System.Collections.Generic;
    using Models.Board;

    /// <summary>
    /// GetAllBoardsQuery result.
    /// </summary>
    public sealed class GetAllBoardsQueryResult
    {
        /// <summary>
        /// Gets or sets the models.
        /// </summary>
        /// <value>
        /// The models.
        /// </value>
        public List<BoardModel> Models { get; set; }
    }
}