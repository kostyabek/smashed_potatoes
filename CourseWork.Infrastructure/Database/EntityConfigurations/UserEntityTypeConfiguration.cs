using CourseWork.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork.Infrastructure.Database.EntityConfigurations
{
    /// <summary>
    /// User entity type configuration.
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;AppUser&gt;" />
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
