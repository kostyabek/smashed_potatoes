namespace CourseWork.Infrastructure.Database
{
    using System.Linq;
    using Extensions;
    using Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

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
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var rel in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                rel.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.AddIdentityRules();
            builder.AddPostgreSqlRules();
        }
    }
}
