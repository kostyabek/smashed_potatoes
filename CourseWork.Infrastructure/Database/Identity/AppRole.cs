namespace CourseWork.Infrastructure.Database.Identity
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class AppRole : IdentityRole<int>
    {
        public List<AppUserRole> UserRoles { get; set; }
    }
}
