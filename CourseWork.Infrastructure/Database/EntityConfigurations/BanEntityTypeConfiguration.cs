using CourseWork.Core.Database.Entities.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork.Core.Database.EntityConfigurations
{
    /// <summary>
    /// Ban Entity Type Configuration.
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;Ban&gt;" />
    public class BanEntityTypeConfiguration : IEntityTypeConfiguration<Ban>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            builder.HasOne(e => e.User)
                .WithMany(e => e.Bans)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(e => e.UserId);
        }
    }
}
