using MediatR;

namespace CourseWork.Core.CQRS
{
    /// <summary>
    /// CQRS command.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IRequest&lt;T&gt;" />
    public interface ICommand<out T> : IRequest<T>
    {
    }
}
