using System.Security.Claims;
using System.Threading.Tasks;
using CourseWork.Application.Interfaces;
using CourseWork.Domain.Identity;
using CourseWork.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Infrastructure.Services
{
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

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
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
    }
}
