namespace CourseWork.Core.Database.EntityConfigurations
{
    using Entities.Replies;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Reply Report Entity Type Configuration.
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;ReplyReport&gt;" />
    public class ReplyReportEntityTypeConfiguration : IEntityTypeConfiguration<ReplyReport>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ReplyReport> builder)
        {
            builder.HasOne(e => e.Reply)
                .WithMany()
                .HasForeignKey(e => e.ReplyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.ReportReason)
                .WithMany()
                .HasForeignKey(e => e.ReportReasonId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.Reporter)
                .WithMany()
                .HasForeignKey(e => e.ReporterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
