namespace CourseWork.Core.Models.Thread
{
    /// <summary>
    /// Popular thread model.
    /// </summary>
    public class PopularThreadModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the board.
        /// </summary>
        /// <value>
        /// The name of the board.
        /// </value>
        public string BoardName { get; set; }

        /// <summary>
        /// Gets or sets the display name of the board.
        /// </summary>
        /// <value>
        /// The display name of the board.
        /// </value>
        public string BoardDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the main picture path.
        /// </summary>
        /// <value>
        /// The main picture path.
        /// </value>
        public string MainPicturePath { get; set; }
    }
}
