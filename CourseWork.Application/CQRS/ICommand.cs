namespace CourseWork.Common.CQRS
{
    using MediatR;

    /// <summary>
    /// CQRS command.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MediatR.IRequest&lt;T&gt;" />
    public interface ICommand<out T> : IRequest<T>
    {
    }
}
