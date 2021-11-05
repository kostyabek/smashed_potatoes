namespace CourseWork.Core.Database.Entities.Files
{
    /// <summary>
    /// Stored image model.
    /// </summary>
    /// <seealso cref="BaseIdentityEntity" />
    public class ImageModel : BaseIdentityEntity
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
    }
}
