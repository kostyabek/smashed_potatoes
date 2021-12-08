using System.Collections.Generic;
using CourseWork.Core.Models.Board;

namespace CourseWork.Core.Queries.Board.GetAllBoards
{
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

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalCount { get; set; }
    }
}