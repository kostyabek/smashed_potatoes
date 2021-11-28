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
            /// The admin.
            /// </summary>
            public static int Admin = 1;

            /// <summary>
            /// The moderator.
            /// </summary>
            public static int Moderator = 2;

            /// <summary>
            /// The user.
            /// </summary>
            public static int User = 3;

            /// <summary>
            /// Creates new user.
            /// </summary>
            public static int NewUser = 4;
        }

        /// <summary>
        /// Reply Report Reasons.
        /// </summary>
        public static class ReplyReportReasons
        {
            /// <summary>
            /// The inappropriate.
            /// </summary>
            public static int Inappropriate = 1;

            /// <summary>
            /// The bullying.
            /// </summary>
            public static int Bullying = 2;

            /// <summary>
            /// The other.
            /// </summary>
            public static int Other = 3;
        }
    }
}
