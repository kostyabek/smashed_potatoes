using System.Collections.Generic;
using CourseWork.Core.Database.Entities.Threads;

namespace CourseWork.Core.Database.Entities.Boards
{
    /// <summary>
    /// Board type.
    /// </summary>
    /// <seealso cref="BaseNamedEntity" />
    public class PotatoBoard : BaseNamedEntity
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the threads.
        /// </summary>
        /// <value>
        /// The threads.
        /// </value>
        public List<PotatoThread> Threads { get; set; }
    }
}
