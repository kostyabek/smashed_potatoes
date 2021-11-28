using CourseWork.Core.Database.Entities.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork.Core.Database.EntityConfigurations
{
    /// <summary>
    /// User Board Subscription Entity Type Configuration.
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;UserBoardSubscription&gt;" />
    public class UserBoardSubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<UserBoardSubscription>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UserBoardSubscription> builder)
        {
            builder.HasOne(e => e.User)
                .WithMany(e => e.BoardSubscriptions)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Board)
                .WithMany()
                .HasForeignKey(e => e.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("UserBoardSubscriptions");
        }
    }
}
