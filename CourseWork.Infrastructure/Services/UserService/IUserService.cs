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
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; }

        /// <summary>
        /// Gets the current user asynchronously.
        /// </summary>
        /// <returns></returns>
        public Task<AppUser> GetCurrentUserAsync();

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<AppUser> GetUserById(int userId);

        /// <summary>
        /// Determines whether [is given user banned] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<bool> IsGivenUserBanned(int userId);

        /// <summary>
        /// Determines whether [is current user banned].
        /// </summary>
        /// <returns></returns>
        public Task<bool> IsCurrentUserBanned();
    }
}
