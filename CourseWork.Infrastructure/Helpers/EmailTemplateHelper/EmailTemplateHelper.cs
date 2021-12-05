namespace CourseWork.Core.Helpers.EmailTemplateHelper
{
    using System.IO;
    using System.Threading.Tasks;
    using RazorEngine;
    using RazorEngine.Templating;

    /// <summary>
    /// Email Template Helper.
    /// </summary>
    public class EmailTemplateHelper : IEmailTemplateHelper
    {
        /// <inheritdoc/>
        public async Task<string> GetEmailTemplateAsString<T>(string viewName, T model)
        {
            var templatePath = @$"{Directory.GetCurrentDirectory()}\Views\{viewName}.cshtml";
            var template = await File.ReadAllTextAsync(templatePath);

            var html = Engine.Razor.RunCompile(template, viewName, typeof(T), model);

            return html;
        }
    }
}