using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using CourseWork.Core.Database.Entities.Files;
using CourseWork.Core.Database.Entities.Replies;

namespace CourseWork.Core.Database.Entities.Identity
{
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

        /// <summary>
        /// Gets or sets the avatar identifier.
        /// </summary>
        /// <value>
        /// The avatar identifier.
        /// </value>
        public int? AvatarId { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        /// <value>
        /// The avatar.
        /// </value>
        public ImageModel Avatar { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the replies.
        /// </summary>
        /// <value>
        /// The replies.
        /// </value>
        public List<PotatoReply> Replies { get; set; }
    }
}
