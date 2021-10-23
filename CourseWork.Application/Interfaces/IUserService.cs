using System.Threading.Tasks;
using CourseWork.Domain.Identity;

namespace CourseWork.Application.Interfaces
{
    /// <summary>
    /// Contains method definitions to be implemented by user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets the current user asynchronously.
        /// </summary>
        /// <returns></returns>
        public Task<AppUser> GetCurrentUserAsync();
    }
}
