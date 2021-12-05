using System.Threading.Tasks;

namespace CourseWork.Core.Services.WeeklySummaryEmailService
{
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
