using MediatR;

namespace CourseWork.Core.CQRS
{
    /// <summary>
    /// Represents CQRS query.
    /// </summary>
    /// <typeparam name="T">Command generic return type.</typeparam>
    /// <seealso cref="IRequest&lt;T&gt;" />
    public interface IQuery<out T> : IRequest<T>
    {
    }
}
