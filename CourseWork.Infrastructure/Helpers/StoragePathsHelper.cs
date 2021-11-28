using System;
using System.IO;
using CourseWork.Common.Consts;

namespace CourseWork.Core.Helpers
{
    using System.Text;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Contains methods for getting image storage paths.
    /// </summary>
    public class StoragePathsHelper
    {
        /// <summary>
        /// Gets the images static files path.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <returns></returns>
        public static StringBuilder GetImagesStaticFilesPath(HttpRequest httpRequest)
        {
            return new StringBuilder($"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/images/");
        }

        /// <summary>
        /// Gets the avatar storage path.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string GetAvatarStoragePath(string fileName)
        {
            var imagesPath = GetAvatarsStoragePath();
            var avatarPath = Path.Combine(imagesPath, fileName);

            return avatarPath;
        }

        /// <summary>
        /// Gets the avatars storage path.
        /// </summary>
        /// <returns></returns>
        public static string GetAvatarsStoragePath()
        {
            var imagesPath = GetImagesPath();
            var avatarImagesDir = Path.Combine(imagesPath, AppConsts.StoragePaths.Avatars);

            if (!Directory.Exists(avatarImagesDir))
            {
                Directory.CreateDirectory(avatarImagesDir);
            }

            return avatarImagesDir;
        }

        private static string GetImagesPath()
        {
            var storagePath = GetBaseStoragePath();
            var imagesDir = Path.Combine(storagePath, "images");

            if (!Directory.Exists(imagesDir))
            {
                Directory.CreateDirectory(imagesDir);
            }

            return imagesDir;
        }

        /// <summary>
        /// Gets the base storage path.
        /// </summary>
        /// <returns></returns>
        private static string GetBaseStoragePath()
        {
            var homePath = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            var appDir = "smashed-potatoes";

            var storageDir = Path.Combine(homePath, appDir);

            if (!Directory.Exists(storageDir))
            {
                Directory.CreateDirectory(storageDir);
            }

            return storageDir;
        }
    }
}
