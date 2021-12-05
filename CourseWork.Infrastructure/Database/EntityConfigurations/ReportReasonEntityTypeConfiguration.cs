using CourseWork.Core.Database.Entities.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork.Core.Database.EntityConfigurations
{
    /// <summary>
    /// Report Reason Entity Type Configuration.
    /// </summary>
    public class ReportReasonEntityTypeConfiguration : IEntityTypeConfiguration<ReportReason>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ReportReason> builder)
        {
            builder.ToTable("ReportReasons");
        }
    }
}
