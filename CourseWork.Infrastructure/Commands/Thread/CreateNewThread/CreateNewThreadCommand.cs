namespace CourseWork.Core.Commands.Thread.CreateNewThread
{
    using CQRS;
    using LS.Helpers.Hosting.API;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// CreateNewThreadCommand.
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class CreateNewThreadCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the board identifier.
        /// </summary>
        /// <value>
        /// The board identifier.
        /// </value>
        public int BoardId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the main picture.
        /// </summary>
        /// <value>
        /// The main picture.
        /// </value>
        public IFormFile MainPicture { get; set; }
    }
}