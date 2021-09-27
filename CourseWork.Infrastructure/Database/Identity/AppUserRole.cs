namespace CourseWork.Infrastructure.Database.Identity
{
    using Microsoft.AspNetCore.Identity;

    public class AppUserRole : IdentityUserRole<int>
    {
        public virtual AppUser User { get; set; }

        public virtual AppRole Role { get; set; }
    }
}
