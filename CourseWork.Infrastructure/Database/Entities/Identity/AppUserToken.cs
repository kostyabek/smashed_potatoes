using Microsoft.AspNetCore.Identity;

namespace CourseWork.Core.Database.Entities.Identity
{
    /// <summary>
    /// App user token.
    /// </summary>
    /// <seealso cref="IdentityUserToken&lt;int&gt;" />
    public class AppUserToken : IdentityUserToken<int>
    {
    }
}
