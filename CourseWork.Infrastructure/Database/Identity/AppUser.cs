namespace CourseWork.Infrastructure.Database.Identity
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class AppUser : IdentityUser<int>
    {
        public List<AppUserRole> UserRoles { get; set; }
    }
}
