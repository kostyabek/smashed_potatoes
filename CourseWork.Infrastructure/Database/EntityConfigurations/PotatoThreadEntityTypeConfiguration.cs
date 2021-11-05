using CourseWork.Core.Database.Entities.Threads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork.Core.Database.EntityConfigurations
{
    /// <summary>
    /// Potato Thread Entity Type Configuration.
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;PotatoThread&gt;" />
    public class PotatoThreadEntityTypeConfiguration : IEntityTypeConfiguration<PotatoThread>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<PotatoThread> builder)
        {
            builder.HasOne(e => e.Board)
                .WithMany(e => e.Threads)
                .HasForeignKey(e => e.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.MainPicture)
                .WithMany()
                .HasForeignKey(e => e.MainPictureId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.Replies)
                .WithOne(e => e.Thread)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.User)
                .WithMany(e => e.Threads);
        }
    }
}
