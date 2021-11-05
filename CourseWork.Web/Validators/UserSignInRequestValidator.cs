using CourseWork.Core.Commands.Auth.UserSignIn;
using FluentValidation;

namespace CourseWork.Web.Validators
{
    /// <summary>
    /// User sign in command validator.
    /// </summary>
    /// <seealso cref="UserSignInRequestValidator" />
    public class UserSignInRequestValidator : AbstractValidator<UserSignInCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSignInRequestValidator"/> class.
        /// </summary>
        public UserSignInRequestValidator()
        {
            RuleFor(r => r.Username)
                .NotEmpty()
                .NotNull();

            RuleFor(r => r.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}
