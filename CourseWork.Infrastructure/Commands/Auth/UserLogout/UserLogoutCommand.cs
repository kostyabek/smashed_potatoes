using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Commands.Auth.UserLogout
{
    using CQRS;

    /// <summary>
    /// UserLogoutCommand.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class UserLogoutCommand : ICommand<ExecutionResult>
    {
    }
}