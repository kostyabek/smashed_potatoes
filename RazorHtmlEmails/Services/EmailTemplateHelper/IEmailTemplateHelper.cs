namespace RazorHtmlEmails.Services.EmailTemplateHelper
{
    using System.Threading.Tasks;

    /// <summary>
    /// Contains method declarations for <see cref="EmailTemplateHelper"/>
    /// </summary>
    public interface IEmailTemplateHelper
    {
        /// <summary>
        /// Gets the email template as string.
        /// </summary>
        /// <typeparam name="T">Data model type.</typeparam>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<string> GetEmailTemplateAsString<T>(string templateName, T model);
    }
}
