using CourseWork.Core.Commands.Admin.IgnoreReplyReports;
using FluentValidation;

namespace CourseWork.Web.Validators.Admin
{
    /// <summary>
    /// Ignore Reply Reports Request Validator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;Core.Commands.Admin.IgnoreReplyReports.IgnoreReplyReportsCommand&gt;" />
    public class IgnoreReplyReportsRequestValidator : AbstractValidator<IgnoreReplyReportsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreReplyReportsRequestValidator"/> class.
        /// </summary>
        public IgnoreReplyReportsRequestValidator()
        {
            RuleFor(e => e.ReportIds)
                .NotEmpty()
                .WithMessage("You have to select at least 1 reply to ignore.");
        }
    }
}
