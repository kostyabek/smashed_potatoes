namespace CourseWork.Infrastructure.Database
{
    using Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
    }
}
