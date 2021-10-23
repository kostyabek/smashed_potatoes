namespace CourseWork.Domain.Identity
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// App user.
    /// </summary>
    /// <seealso cref="IdentityUser&lt;int&gt;" />
    public class AppUser : IdentityUser<int>
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
