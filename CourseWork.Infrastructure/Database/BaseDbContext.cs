﻿using System.Linq;
using CourseWork.Core.Database.Entities;
using CourseWork.Core.Database.Entities.Identity;
using CourseWork.Core.Database.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Core.Database
{
    using Entities.Boards;
    using Entities.Files;
    using Entities.Replies;
    using Entities.Threads;

    /// <summary>
    /// Database context of the application.
    /// </summary>
    public class BaseDbContext : IdentityDbContext<
    AppUser,
    AppRole,
    int,
    AppUserClaim,
    AppUserRole,
    AppUserLogin,
    AppRoleClaim,
    AppUserToken>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public DbSet<ImageModel> Images { get; set; }

        /// <summary>
        /// Gets or sets the boards.
        /// </summary>
        /// <value>
        /// The boards.
        /// </value>
        public DbSet<PotatoBoard> Boards { get; set; }

        /// <summary>
        /// Gets or sets the threads.
        /// </summary>
        /// <value>
        /// The threads.
        /// </value>
        public DbSet<PotatoThread> Threads { get; set; }

        /// <summary>
        /// Gets or sets the replies.
        /// </summary>
        /// <value>
        /// The replies.
        /// </value>
        public DbSet<PotatoReply> Replies { get; set; }

        /// <summary>
        /// Gets or sets the reply replies.
        /// </summary>
        /// <value>
        /// The reply replies.
        /// </value>
        public DbSet<ReplyReply> ReplyReplies { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var rel in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                rel.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.ApplyConfigurations();
            builder.AddIdentityRules();
            builder.AddPostgreSqlRules();
            builder.ApplySeed();
        }
    }
}
