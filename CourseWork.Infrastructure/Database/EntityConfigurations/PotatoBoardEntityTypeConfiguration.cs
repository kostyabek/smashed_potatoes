using CourseWork.Core.Database.Entities.Boards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork.Core.Database.EntityConfigurations
{
    /// <summary>
    /// Potato Board Entity Type Configuration.
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;PotatoBoard&gt;" />
    public class PotatoBoardEntityTypeConfiguration : IEntityTypeConfiguration<PotatoBoard>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PotatoBoard> builder)
        {
            builder.HasMany(e => e.Threads)
                .WithOne(t => t.Board)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(e => e.Name).IsUnique();
            builder.HasIndex(e => e.DisplayName).IsUnique();
        }
    }
}
