using CourseWork.Core.Database.Entities.Replies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork.Core.Database.EntityConfigurations
{
    /// <summary>
    /// Potato Reply Entity Type Configuration.
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;PotatoReply&gt;" />
    public class PotatoReplyEntityTypeConfiguration : IEntityTypeConfiguration<PotatoReply>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PotatoReply> builder)
        {
            builder.HasOne(e => e.User)
                .WithMany(e => e.Replies)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Thread)
                .WithMany(e => e.Replies)
                .HasForeignKey(e => e.ThreadId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.PicRelated)
                .WithMany()
                .HasForeignKey(e => e.PicRelatedId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
