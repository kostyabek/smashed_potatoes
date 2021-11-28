namespace CourseWork.Core.Commands.Reply.CreateNewReply
{
    using System.Collections.Generic;
    using Attributes;
    using CQRS;
    using LS.Helpers.Hosting.API;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// CreateNewReplyCommand
    /// </summary>
    /// <seealso cref="ICommand{ExecutionResult}" />
    /// <inheritdoc />
    public sealed class CreateNewReplyCommand : ICommand<ExecutionResult>
    {
        /// <summary>
        /// Gets or sets the thread identifier.
        /// </summary>
        /// <value>
        /// The thread identifier.
        /// </value>
        public int ThreadId { get; set; }

        /// <summary>
        /// Gets or sets the pic related.
        /// </summary>
        /// <value>
        /// The pic related.
        /// </value>
        [AllowedFiles(new[] { ".jpg", ".jpeg", ".png", ".gif" }, 10485760)]
        public IFormFile PicRelated { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the replied to ids.
        /// </summary>
        /// <value>
        /// The replied to ids.
        /// </value>
        public List<int> RepliedToIds { get; set; }
            = new ();
    }
}