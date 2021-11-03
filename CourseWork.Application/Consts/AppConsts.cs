namespace CourseWork.Common.Consts
{
    /// <summary>
    /// Contains application-wide constants.
    /// </summary>
    public static class AppConsts
    {
        /// <summary>
        /// Contains storage directories to different types of files.
        /// </summary>
        public static class StoragePaths
        {
            /// <summary>
            /// The avatars.
            /// </summary>
            public static string Avatars = "avatars";
        }

        /// <summary>
        /// User roles.
        /// </summary>
        public static class UserRoles
        {
            /// <summary>
            /// Creates new user.
            /// </summary>
            public static int NewUser = 1;

            /// <summary>
            /// The user,
            /// </summary>
            public static int User = 2;

            /// <summary>
            /// The moderator,
            /// </summary>
            public static int Moderator = 3;

            /// <summary>
            /// The admin,
            /// </summary>
            public static int Admin = 4;
        }
    }
}
