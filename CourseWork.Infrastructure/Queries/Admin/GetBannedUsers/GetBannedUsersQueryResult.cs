using System.Collections.Generic;
using CourseWork.Core.Models.Admin;

namespace CourseWork.Core.Queries.Admin.GetBannedUsers
{
    /// <summary>
    /// GetBannedUsersQuery result.
    /// </summary>
    public sealed class GetBannedUsersQueryResult
    {
        /// <summary>
        /// Gets or sets the models.
        /// </summary>
        /// <value>
        /// The models.
        /// </value>
        public List<BannedUserListItemModel> Models { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public long TotalCount { get; set; }
    }
}