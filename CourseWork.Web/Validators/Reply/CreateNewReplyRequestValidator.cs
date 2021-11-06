namespace CourseWork.Web.Validators.Reply
{
    using Core.Commands.Reply.CreateNewReply;
    using FluentValidation;

    /// <summary>
    /// Create New Reply Request Validator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;Core.Commands.Reply.CreateNewReply.CreateNewReplyCommand&gt;" />
    public class CreateNewReplyRequestValidator : AbstractValidator<CreateNewReplyCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewReplyRequestValidator"/> class.
        /// </summary>
        public CreateNewReplyRequestValidator()
        {
            RuleFor(e => e.ThreadId)
                .GreaterThanOrEqualTo(1);

            RuleFor(e => e.Content)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(200);

            RuleFor(e => e.RepliedToIds)
                .NotEmpty()
                .Must(e => e.Count < 6);
        }
    }
}
