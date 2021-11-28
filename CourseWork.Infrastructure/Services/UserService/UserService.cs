using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork.Core.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CourseWork.Core.Database.Entities.Identity;

namespace CourseWork.Core.Services.UserService
{
    using System.Linq;

    /// <summary>
    /// Contains methods for user management.
    /// </summary>
    /// <seealso cref="IUserService" />
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BaseDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="dbContext">The database context.</param>
        public UserService(IHttpContextAccessor httpContextAccessor, BaseDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public int UserId
        {
            get
            {
                var value = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return value is null ? 0 : int.Parse(value);
            }
        }

        /// <inheritdoc/>
        public async Task<AppUser> GetCurrentUserAsync()
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<AppUser> GetUserById(int userId)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(e => e.Id == userId);
        }

        /// <summary>
        /// Determines whether [is user banned] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is user banned] [the specified user identifier]; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> IsGivenUserBanned(int userId)
        {
            return await _dbContext.Users.AnyAsync(e => e.Id == userId && e.Bans.Any(b => b.IsActive));
        }

        /// <summary>
        /// Determines whether [is user banned] [the specified user identifier].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is user banned] [the specified user identifier]; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> IsCurrentUserBanned()
        {
            return await _dbContext.Users.AnyAsync(e => e.Id == UserId && e.Bans.Any(b => b.IsActive));
        }
    }
}
