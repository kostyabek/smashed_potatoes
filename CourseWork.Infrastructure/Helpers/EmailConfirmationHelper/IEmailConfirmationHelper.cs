using System.Threading.Tasks;
using CourseWork.Core.Database.Entities.Identity;

namespace CourseWork.Core.Helpers.EmailConfirmationHelper
{
    /// <summary>
    /// Contains method declarations for <see cref="EmailConfirmationHelper"/>.
    /// </summary>
    public interface IEmailConfirmationHelper
    {
        /// <summary>
        /// Sends the email confirmation link.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task SendEmailConfirmationLink(AppUser user);
    }
}
