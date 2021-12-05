using System.ComponentModel.DataAnnotations;

namespace CourseWork.Core.Database.Entities
{
    /// <summary>
    /// Database basic named entity.
    /// </summary>
    /// <seealso cref="BaseIdentityEntity" />
    public class BaseNamedEntity : BaseIdentityEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [StringLength(250)]
        public string Name { get; set; }
    }
}
