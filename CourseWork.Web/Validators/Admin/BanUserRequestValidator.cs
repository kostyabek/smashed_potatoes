using System;
using CourseWork.Core.Commands.Admin.BanUser;
using FluentValidation;

namespace CourseWork.Web.Validators.Admin
{
    /// <summary>
    /// Ban User Request Validator.
    /// </summary>
    /// <seealso cref="BanUserCommand" />
    public class BanUserRequestValidator : AbstractValidator<BanUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BanUserRequestValidator"/> class.
        /// </summary>
        public BanUserRequestValidator()
        {
            RuleFor(e => e.BannedUntil)
                .Must(e => e.HasValue && (int)(e.Value - DateTime.UtcNow).TotalMinutes >= 5)
                .WithMessage("Ban timespan must be at least 5 minutes.");
        }
    }
}
