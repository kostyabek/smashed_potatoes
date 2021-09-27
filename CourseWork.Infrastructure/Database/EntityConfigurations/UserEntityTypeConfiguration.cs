namespace CourseWork.Infrastructure.Database.EntityConfigurations
{
    using Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
