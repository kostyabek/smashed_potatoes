using CourseWork.Infrastructure.Commands.UserSignUpCommand;
using FluentValidation;

namespace CourseWork.Web.Validators
{
    /// <summary>
    /// User sign up data validator.
    /// </summary>
    /// <seealso cref="UserSignUpCommand" />
    public class UserSignUpValidator : AbstractValidator<UserSignUpCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSignUpValidator"/> class.
        /// </summary>
        public UserSignUpValidator()
        {
            RuleFor(u => u.Username)
                .Matches("^(?=.{4,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
                .WithMessage("Username must contain letters, numbers, underscores or dots and be from 4 to 20 characters long.");

            RuleFor(e => e.Email)
                .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")
                .WithMessage("Wrong email format!");

            RuleFor(e => e.Password)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")
                .WithMessage("Password must consist of lower and uppercase letters, at least one number, one special character(@$!%*?&) and be 8+ characters long");
        }
    }
}
