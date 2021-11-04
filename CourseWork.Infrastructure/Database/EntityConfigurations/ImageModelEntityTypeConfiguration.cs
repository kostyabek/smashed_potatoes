using CourseWork.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork.Core.Database.EntityConfigurations
{
    /// <summary>
    /// Image Model Entity Type Configuration.
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;ImageModel&gt;" />
    public class ImageModelEntityTypeConfiguration : IEntityTypeConfiguration<ImageModel>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ImageModel> builder)
        {
            builder.ToTable("Images");
        }
    }
}
