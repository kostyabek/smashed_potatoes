using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CourseWork.Common.Configurations;
using CourseWork.Core.Database.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace CourseWork.Core.Helpers.EmailConfirmationHelper
{
    /// <summary>
    /// Email Confirmation Helper.
    /// </summary>
    public class EmailConfirmationHelper : IEmailConfirmationHelper
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;
        private readonly IOptions<SmtpClientCredentials> _smtpClientCredentials;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailConfirmationHelper" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="linkGenerator">The link generator.</param>
        /// <param name="smtpClientCredentials">The SMTP client credentials.</param>
        public EmailConfirmationHelper(
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            LinkGenerator linkGenerator,
            IOptions<SmtpClientCredentials> smtpClientCredentials)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
            _smtpClientCredentials = smtpClientCredentials;
        }

        /// <inheritdoc/>
        public async Task SendEmailConfirmationLink(AppUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink =
                _linkGenerator.GetUriByName(
                    _httpContextAccessor.HttpContext,
                    "ConfirmEmail",
                    new { token, userId = user.Id });

            var smtpClient = PrepareSmtpClient();

            var body = $"Here is your e-mail confirmation link:\n{confirmationLink}";
            var emailMessage = new MailMessage
            {
                From = new MailAddress("unitedfamilyofficial@gmail.com", "Smashed Potatoes"),
                Subject = "Email Confirmation",
                IsBodyHtml = false,
                Body = body,
                Priority = MailPriority.Normal,
            };

            emailMessage.To.Add(new MailAddress(user.Email));

            smtpClient.SendAsync(emailMessage, null);
        }

        private SmtpClient PrepareSmtpClient()
        {
            var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpClientCredentials.Value.Login, _smtpClientCredentials.Value.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            return client;
        }
    }
}
