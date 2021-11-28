using CourseWork.Core.Database.Entities.Replies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork.Core.Database.EntityConfigurations
{
    /// <summary>
    /// ReplyReply Entity Type Configuration.
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;ReplyReply&gt;" />
    public class ReplyReplyEntityTypeConfiguration : IEntityTypeConfiguration<ReplyReply>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ReplyReply> builder)
        {
            builder.HasKey(e => new { e.PointingReplyId, e.PointedReplyId });

            builder.HasOne(e => e.PointingReply)
                .WithMany(e => e.ReplyReplies)
                .HasForeignKey(e => e.PointingReplyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.PointedReply)
                .WithMany()
                .HasForeignKey(e => e.PointedReplyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
