using CourseWork.Core.Database.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork.Core.Database.EntityConfigurations
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

            builder.HasOne(e => e.Avatar)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.Replies)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            builder.HasMany(e => e.Threads)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Replies)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
        }
    }
}
