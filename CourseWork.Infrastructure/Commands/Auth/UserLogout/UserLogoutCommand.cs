using CourseWork.Common.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Commands.Auth.UserLogout
{
    /// <summary>
    /// UserLogoutCommand.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class UserLogoutCommand : ICommand<ExecutionResult>
    {
    }
}