using Microsoft.AspNetCore.Identity;

namespace CourseWork.Domain.Identity
{
    /// <summary>
    /// App user role.
    /// </summary>
    /// <seealso cref="IdentityUserRole&lt;int&gt;" />
    public class AppUserRole : IdentityUserRole<int>
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual AppUser User { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public virtual AppRole Role { get; set; }
    }
}
