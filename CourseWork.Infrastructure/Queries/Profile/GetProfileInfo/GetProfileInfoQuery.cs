using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Queries.Profile.GetProfileInfo
{
    /// <inheritdoc />
    /// <summary>
    /// GetProfileInfoQuery.
    /// </summary>
    /// <seealso cref="T:CQRS.IQuery`1" />
    public sealed class GetProfileInfoQuery : IQuery<ExecutionResult<GetProfileInfoQueryResult>>
    {
    }
}