namespace CourseWork.Web.Validators.Profile
{
    using Core.Commands.Profile.ChangeAvatar;
    using FluentValidation;

    /// <summary>
    /// Chang eAvatar Request Validator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;Core.Commands.Profile.ChangeAvatar.ChangeAvatarCommand&gt;" />
    public class ChangeAvatarRequestValidator : AbstractValidator<ChangeAvatarCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeAvatarRequestValidator"/> class.
        /// </summary>
        public ChangeAvatarRequestValidator()
        {
            RuleFor(r => r.Avatar)
                .NotNull();
        }
    }
}
