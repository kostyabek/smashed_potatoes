namespace CourseWork.Core.Services.BannedUsersDeletionService
{
    using System.Threading.Tasks;

    /// <summary>
    /// Contains method declarations for <see cref="WeeklySummaryEmailService"/>.
    /// </summary>
    public interface IWeeklySummaryEmailService
    {
        /// <summary>
        /// Deletes the users.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SendEmails();
    }
}
