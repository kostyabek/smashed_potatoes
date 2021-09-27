namespace CourseWork.Application.CQRS
{
    using MediatR;

    public interface IQuery<out T>: IRequest<T>
    {
    }
}
