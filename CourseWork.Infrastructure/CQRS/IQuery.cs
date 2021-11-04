namespace CourseWork.Common.CQRS
{
    using MediatR;

    /// <summary>
    /// Represents CQRS query.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MediatR.IRequest&lt;T&gt;" />
    public interface IQuery<out T>: IRequest<T>
    {
    }
}
