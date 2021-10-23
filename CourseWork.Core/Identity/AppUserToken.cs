namespace CourseWork.Domain.Identity
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// App user token.
    /// </summary>
    /// <seealso cref="IdentityUserToken&lt;int&gt;" />
    public class AppUserToken : IdentityUserToken<int>
    {
    }
}
