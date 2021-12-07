using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Commands.Auth.ResendUserEmailConfirmationLink
{
    /// <summary>
    /// ResendUserEmailConfirmationLinkCommand.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class ResendUserEmailConfirmationLinkCommand : ICommand<ExecutionResult>
    {
    }
}