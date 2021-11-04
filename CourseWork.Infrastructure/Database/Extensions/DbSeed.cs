using System;
using System.Collections.Generic;
using CourseWork.Common.Consts;
using CourseWork.Core.Database.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Core.Database.Extensions
{
    /// <summary>
    /// Contains methods for database seed.
    /// </summary>
    public static class DbSeed
    {
        /// <summary>
        /// Adds the roles seed.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        /// <returns></returns>
        public static ModelBuilder AddRolesSeed(this ModelBuilder modelBuilder)
        {
            var roles = new List<AppRole>
            {
                new ()
                {
                    Id = AppConsts.UserRoles.Admin,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new ()
                {
                    Id = AppConsts.UserRoles.Moderator,
                    Name = "Moderator",
                    NormalizedName = "Moderator".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new ()
                {
                    Id = AppConsts.UserRoles.User,
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new ()
                {
                    Id = AppConsts.UserRoles.NewUser,
                    Name = "New user",
                    NormalizedName = "New user".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
            }.ToArray();

            modelBuilder.Entity<AppRole>().HasData(roles);

            return modelBuilder;
        }
    }
}
