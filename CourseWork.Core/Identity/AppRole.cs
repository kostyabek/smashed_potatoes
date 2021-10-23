namespace CourseWork.Domain.Identity
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// App role.
    /// </summary>
    /// <seealso cref="IdentityRole&lt;int&gt;" />
    public class AppRole : IdentityRole<int>
    {
        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        /// <value>
        /// The user roles.
        /// </value>
        public List<AppUserRole> UserRoles { get; set; }
    }
}
