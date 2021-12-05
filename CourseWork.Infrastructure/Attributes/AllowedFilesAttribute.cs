using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CourseWork.Core.Attributes
{
    /// <summary>
    /// Allowed file extensions attribute.
    /// </summary>
    /// <seealso cref="ValidationAttribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class AllowedFilesAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        private readonly int _allowedFileSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllowedFilesAttribute" /> class.
        /// </summary>
        /// <param name="extensions">The extensions.</param>
        /// <param name="allowedFileSize">Size of the allowed file.</param>
        public AllowedFilesAttribute(string[] extensions, int allowedFileSize)
        {
            _extensions = extensions;
            _allowedFileSize = allowedFileSize;
        }

        /// <summary>
        /// Gets the file extension error message.
        /// </summary>
        /// <returns></returns>
        public string GetFileExtensionErrorMessage()
        {
            return "The file extension of the provided image is not supported.";
        }

        /// <summary>
        /// Gets the file size error message.
        /// </summary>
        /// <returns></returns>
        public string GetFileSizeErrorMessage()
        {
            return "The file size of the provided image is too big.";
        }

        /// <inheritdoc/>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetFileExtensionErrorMessage());
                }

                if (file.Length > _allowedFileSize)
                {
                    return new ValidationResult(GetFileSizeErrorMessage());
                }
            }

            return ValidationResult.Success;
        }
    }
}
