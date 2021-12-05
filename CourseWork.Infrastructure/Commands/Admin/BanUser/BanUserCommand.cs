using System;
using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Commands.Admin.BanUser
{
    /// <summary>
    /// BanUserCommand.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class BanUserCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the banned until.
        /// </summary>
        /// <value>
        /// The banned until.
        /// </value>
        public DateTime? BannedUntil { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ban permanent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is ban permanent; otherwise, <c>false</c>.
        /// </value>
        public bool IsBanPermanent { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; set; }
    }
}