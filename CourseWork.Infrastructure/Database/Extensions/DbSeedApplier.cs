namespace CourseWork.Core.Database.Extensions
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Contains method for centralized DB seed methods usage.
    /// </summary>
    public static class DbSeedApplier
    {
        /// <summary>
        /// Applies the seed.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        /// <returns></returns>
        public static ModelBuilder ApplySeed(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .AddRolesSeed()
                .AddReplyReportReasonsSeed();

            return modelBuilder;
        }
    }
}
