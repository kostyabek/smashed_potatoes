using System.Collections.Generic;
using CourseWork.Common.Consts;
using CourseWork.Core.Database.Entities.Identity;
using CourseWork.Core.Database.Entities.Reports;
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
                    ConcurrencyStamp = "2e6e4c4b-43d0-4a0d-9a9c-320c24260476",
                },
                new ()
                {
                    Id = AppConsts.UserRoles.User,
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                    ConcurrencyStamp = "07c3c173-38c0-444a-a5a6-511507b5e590",
                },
                new ()
                {
                    Id = AppConsts.UserRoles.NewUser,
                    Name = "New user",
                    NormalizedName = "New user".ToUpper(),
                    ConcurrencyStamp = "ab109c0f-d32f-4615-9ae2-b928afdc8088",
                },
            }.ToArray();

            modelBuilder.Entity<AppRole>().HasData(roles);

            return modelBuilder;
        }

        /// <summary>
        /// Adds the reply report reasons seed.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        /// <returns></returns>
        public static ModelBuilder AddReplyReportReasonsSeed(this ModelBuilder modelBuilder)
        {
            var reportReasons = new List<ReportReason>
            {
                new ()
                {
                    Id = AppConsts.ReplyReportReasons.Inappropriate,
                    Name = "Inappropriate",
                },
                new ()
                {
                    Id = AppConsts.ReplyReportReasons.Bullying,
                    Name = "Bullying",
                },
                new ()
                {
                    Id = AppConsts.ReplyReportReasons.Other,
                    Name = "Other",
                },
            }.ToArray();

            modelBuilder.Entity<ReportReason>().HasData(reportReasons);

            return modelBuilder;
        }
    }
}
