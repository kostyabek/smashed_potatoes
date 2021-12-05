using System.Linq;
using System.Threading.Tasks;
using CourseWork.Core.Database.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.Core.Extensions
{
    /// <summary>
    /// Sign in manager extensions.
    /// </summary>
    public static class SignInManagerExtensions
    {
        /// <summary>
        /// Passwords the sign in with ban check asynchronous.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="isPersistent">if set to <c>true</c> [is persistent].</param>
        /// <param name="lockoutOnFailure">if set to <c>true</c> [lockout on failure].</param>
        /// <returns></returns>
        public static async Task<SignInResult> PasswordSignInWithBanCheckAsync(
            this SignInManager<AppUser> manager,
            AppUser user,
            string password,
            bool isPersistent,
            bool lockoutOnFailure)
        {
            if (user.Bans.Any(e => e.IsActive))
            {
                return SignInResult.NotAllowed;
            }

            return await manager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }
    }
}
