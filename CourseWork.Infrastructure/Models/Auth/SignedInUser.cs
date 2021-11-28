namespace CourseWork.Core.Models.Auth
{
    using System.Collections.Generic;

    /// <summary>
    /// Signed In User model.
    /// </summary>
    public class SignedInUser
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public List<string> Roles { get; set; }
    }
}
