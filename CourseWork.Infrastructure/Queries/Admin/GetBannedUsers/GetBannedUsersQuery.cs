using System;
using CourseWork.Application.Pagination;
using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Queries.Admin.GetBannedUsers
{
    /// <summary>
    /// GetBannedUsersQuery.
    /// </summary>
    /// <seealso cref="T:CQRS.IQuery`1" />
    public sealed class GetBannedUsersQuery : PaginatedQuery, IQuery<ExecutionResult<GetBannedUsersQueryResult>>
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the ban end date.
        /// </summary>
        /// <value>
        /// The ban end date.
        /// </value>
        public DateTime? BanEndDate { get; set; }
    }
}