using CourseWork.Core.Commands.Board.CreateNewBoard;
using FluentValidation;

namespace CourseWork.Web.Validators.Board
{
    /// <summary>
    /// Validator for new board requests.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;Core.Commands.Board.CreateNewBoard.CreateNewBoardCommand&gt;" />
    public class CreateNewBoardRequestValidator : AbstractValidator<CreateNewBoardCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewBoardRequestValidator"/> class.
        /// </summary>
        public CreateNewBoardRequestValidator()
        {
            RuleFor(e => e.Name)
                .NotNull()
                .NotEmpty()
                .Matches("[a-zA-Z]{2,3}")
                .WithMessage("Board name must consist of 2 or 3 letters.");

            RuleFor(e => e.DisplayName)
                .NotNull()
                .NotEmpty()
                .Matches("[a-zA-Z0-9]{3,100}")
                .WithMessage("Board display name must consist of letters and numbers only and be 3-100 letters long.");
        }
    }
}
