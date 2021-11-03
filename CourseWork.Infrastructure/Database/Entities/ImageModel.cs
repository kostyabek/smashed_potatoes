namespace CourseWork.Core.Database.Entities
{
    /// <summary>
    /// Stored image model.
    /// </summary>
    /// <seealso cref="BaseIdentityEntity" />
    public class ImageModel : BaseIdentityEntity
    {
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath { get; set; }
    }
}
