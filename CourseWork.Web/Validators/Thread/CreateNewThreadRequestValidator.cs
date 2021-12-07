using CourseWork.Core.Commands.Thread.CreateNewThread;
using FluentValidation;

namespace CourseWork.Web.Validators.Thread
{
    /// <summary>
    /// Create New Thread Request Validator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;Core.Commands.Thread.CreateNewThread.CreateNewThreadCommand&gt;" />
    public class CreateNewThreadRequestValidator : AbstractValidator<CreateNewThreadCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewThreadRequestValidator"/> class.
        /// </summary>
        public CreateNewThreadRequestValidator()
        {
            RuleFor(e => e.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(e => e.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(200);

            RuleFor(e => e.BoardId)
                .GreaterThanOrEqualTo(1);

            RuleFor(e => e.MainPicture)
                .NotNull();
        }
    }
}
