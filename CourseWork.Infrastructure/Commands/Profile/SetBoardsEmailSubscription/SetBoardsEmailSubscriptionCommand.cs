namespace CourseWork.Core.Commands.Profile.SetBoardsEmailSubscription
{
    using System.Collections.Generic;
    using CQRS;
    using LS.Helpers.Hosting.API;

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