using System.Collections.Generic;
using CourseWork.Core.Database.Entities.Admin;
using CourseWork.Core.Database.Entities.Files;
using CourseWork.Core.Database.Entities.Profile;
using CourseWork.Core.Database.Entities.Replies;
using CourseWork.Core.Database.Entities.Threads;
using Microsoft.AspNetCore.Identity;

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
        /// Gets or sets the threads.
        /// </summary>
        /// <value>
        /// The threads.
        /// </value>
        public List<PotatoThread> Threads { get; set; }

        /// <summary>
        /// Gets or sets the replies.
        /// </summary>
        /// <value>
        /// The replies.
        /// </value>
        public List<PotatoReply> Replies { get; set; }

        /// <summary>
        /// Gets or sets the bans.
        /// </summary>
        /// <value>
        /// The bans.
        /// </value>
        public List<Ban> Bans { get; set; }

        /// <summary>
        /// Gets or sets the board subscriptions.
        /// </summary>
        /// <value>
        /// The board subscriptions.
        /// </value>
        public List<UserBoardSubscription> BoardSubscriptions { get; set; }
    }
}
