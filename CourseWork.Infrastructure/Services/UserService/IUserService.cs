using System.Threading.Tasks;
using CourseWork.Core.Database.Entities.Identity;

namespace CourseWork.Core.Services.UserService
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
