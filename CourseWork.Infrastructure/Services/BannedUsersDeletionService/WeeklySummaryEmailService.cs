namespace CourseWork.Core.Services.BannedUsersDeletionService
{
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Common.Configurations;
    using Database;
    using Database.Entities.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Weekly Summary Email Service.
    /// </summary>
    /// <seealso cref="IWeeklySummaryEmailService" />
    public class WeeklySummaryEmailService : IWeeklySummaryEmailService
    {
        private readonly BaseDbContext _dbContext;
        private readonly IOptions<SmtpClientCredentials> _smtpClientCredentials;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeeklySummaryEmailService" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="smtpClientCredentials">The SMTP client credentials.</param>
        public WeeklySummaryEmailService(BaseDbContext dbContext,
            IOptions<SmtpClientCredentials> smtpClientCredentials)
        {
            _dbContext = dbContext;
            _smtpClientCredentials = smtpClientCredentials;
        }

        /// <inheritdoc/>
        public async Task SendEmails()
        {
            var users = await _dbContext
                .Users
                .AsNoTracking()
                .Where(e => e.BoardSubscriptions.Any())
                .ToListAsync();

            var smtpClient = PrepareSmtpClient();

            foreach (var user in users)
            {
                var message = PrepareEmailData(user);
                smtpClient.SendAsync(message, null);
            }
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

        private MailMessage PrepareEmailData(AppUser user)
        {
            var emailMessage = new MailMessage
            {
                From = new MailAddress("unitedfamilyofficial@gmail.com", "Smashed Potatoes"),
                Subject = "Weekly summary",
            };

            emailMessage.To.Add(new MailAddress(user.Email));
            return emailMessage;
        }
    }
}
