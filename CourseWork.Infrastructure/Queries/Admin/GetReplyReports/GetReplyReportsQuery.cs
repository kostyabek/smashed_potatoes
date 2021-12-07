using CourseWork.Application.Pagination;
using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Queries.Admin.GetReplyReports
{
    using System;

    /// <summary>
    /// GetReplyReportsQuery.
    /// </summary>
    /// <seealso cref="T:CQRS.IQuery`1" />
    public sealed class GetReplyReportsQuery : PaginatedQuery, IQuery<ExecutionResult<GetReplyReportsQueryResult>>
    {
        /// <summary>
        /// Gets or sets the board identifier.
        /// </summary>
        /// <value>
        /// The board identifier.
        /// </value>
        public int? BoardId { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime? Date { get; set; }
    }
}