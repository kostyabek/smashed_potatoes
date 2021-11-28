namespace CourseWork.Web.Validators.Auth
{
    using Core.Commands.Auth.UserSignUp;
    using FluentValidation;

    /// <summary>
    /// User sign up data validator.
    /// </summary>
    /// <seealso cref="UserSignUpCommand" />
    public class UserSignUpRequestValidator : AbstractValidator<UserSignUpCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSignUpRequestValidator"/> class.
        /// </summary>
        public UserSignUpRequestValidator()
        {
            RuleFor(e => e.Username)
                .Matches("^(?=.{4,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
                .WithMessage("Username can contain letters, numbers, underscores or dots and be from 4 to 20 characters long.");

            RuleFor(e => e.DisplayName)
                .Matches("^(?=.{4,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
                .WithMessage("Display name can contain letters, numbers, underscores or dots and be from 4 to 20 characters long.");

            RuleFor(e => e.Email)
                .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")
                .WithMessage("Wrong email format!");

            RuleFor(e => e.Password)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")
                .WithMessage("Password must consist of lower and uppercase letters, at least one number, one special character(@$!%*?&) and be 8+ characters long");
        }
    }
}
