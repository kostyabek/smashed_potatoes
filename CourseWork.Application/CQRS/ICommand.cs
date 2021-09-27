namespace CourseWork.Application.CQRS
{
    using MediatR;

    public interface ICommand<out T> : IRequest<T>
    {
    }
}
