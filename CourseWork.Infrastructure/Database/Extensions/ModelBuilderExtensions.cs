﻿namespace CourseWork.Infrastructure.Database.Extensions
{
    using Domain.Identity;
    using LS.Helpers.Hosting.Extensions;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Contains extension methods for <see cref="ModelBuilder"/>.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Adds the postgre SQL rules.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void AddPostgreSqlRules(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                if (entity.BaseType == null)
                {
                    entity.SetTableName(entity.GetTableName().ToSnakeCase());
                }

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToSnakeCase());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
                }
            }
        }

        /// <summary>
        /// Adds the identity rules.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void AddIdentityRules(this ModelBuilder builder)
        {
            builder.Entity<AppUser>(e => e.ToTable("Users"));
            builder.Entity<AppRole>(e => e.ToTable("Roles"));
            builder.Entity<AppUserRole>(e => e.ToTable("UsersRoles"));
            builder.Entity<AppUserLogin>(e => e.ToTable("UsersLogins"));
            builder.Entity<AppUserClaim>(e => e.ToTable("UsersClaims"));
            builder.Entity<AppUserToken>(e => e.ToTable("UsersTokens"));
            builder.Entity<AppRoleClaim>(e => e.ToTable("RolesClaims"));

            builder.Entity<AppUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(x => x.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(x => x.RoleId)
                    .IsRequired();

                userRole.HasOne(x => x.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired();
            });
        }
    }
}