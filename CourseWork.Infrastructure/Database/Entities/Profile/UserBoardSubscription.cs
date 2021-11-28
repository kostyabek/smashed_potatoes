namespace CourseWork.Core.Database.Entities.Profile
{
    using Boards;
    using Identity;

    /// <summary>
    /// User Board Subscription.
    /// </summary>
    /// <seealso cref="BaseIdentityEntity" />
    public class UserBoardSubscription : BaseIdentityEntity
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public AppUser User { get; set; }

        /// <summary>
        /// Gets or sets the board identifier.
        /// </summary>
        /// <value>
        /// The board identifier.
        /// </value>
        public int BoardId { get; set; }

        /// <summary>
        /// Gets or sets the board.
        /// </summary>
        /// <value>
        /// The board.
        /// </value>
        public PotatoBoard Board { get; set; }
    }
}
