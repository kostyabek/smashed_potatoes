namespace CourseWork.Core.Database.Entities.Files
{
    using System;

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

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime Created { get; set; }
    }
}
