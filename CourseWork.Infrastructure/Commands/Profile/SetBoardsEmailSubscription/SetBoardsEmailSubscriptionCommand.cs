using System.Collections.Generic;
using CourseWork.Core.CQRS;
using LS.Helpers.Hosting.API;

namespace CourseWork.Core.Commands.Profile.SetBoardsEmailSubscription
{
    /// <summary>
    /// SetBoardsEmailSubscriptionCommand.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class SetBoardsEmailSubscriptionCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the board ids.
        /// </summary>
        /// <value>
        /// The board ids.
        /// </value>
        public List<int> BoardIds { get; set; }
    }
}