using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using CourseWork.Common.Configurations;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities.Boards;
using CourseWork.Core.Database.Entities.Files;
using CourseWork.Core.Database.Entities.Identity;
using CourseWork.Core.Database.Entities.Replies;
using CourseWork.Core.Database.Entities.Threads;
using CourseWork.Core.Helpers;
using CourseWork.Core.Helpers.DatabaseConnectionHelper;
using CourseWork.Core.Helpers.EmailTemplateHelper;
using CourseWork.Core.Models.EmailTemplate;
using CourseWork.Core.Models.Reply;
using Dapper;
using LS.Helpers.Hosting.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CourseWork.Core.Services.WeeklySummaryEmailService
{
    /// <summary>
    /// Weekly Summary Email Service.
    /// </summary>
    /// <seealso cref="IWeeklySummaryEmailService" />
    public class WeeklySummaryEmailService : IWeeklySummaryEmailService
    {
        private readonly BaseDbContext _dbContext;
        private readonly IOptions<SmtpClientCredentials> _smtpClientCredentials;
        private readonly IEmailTemplateHelper _emailTemplateHelper;
        private readonly IDatabaseConnectionHelper _databaseConnectionHelper;
        private readonly ILogger<WeeklySummaryEmailService> _logger;
        private readonly IOptions<HostingSettings> _hostingSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeeklySummaryEmailService" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="smtpClientCredentials">The SMTP client credentials.</param>
        /// <param name="emailTemplateHelper">The en email template helper.</param>
        /// <param name="databaseConnectionHelper">The database connection helper.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="hostingSettings">The hosting settings.</param>
        public WeeklySummaryEmailService(BaseDbContext dbContext,
            IOptions<SmtpClientCredentials> smtpClientCredentials,
            IEmailTemplateHelper emailTemplateHelper,
            IDatabaseConnectionHelper databaseConnectionHelper,
            ILogger<WeeklySummaryEmailService> logger,
            IOptions<HostingSettings> hostingSettings)
        {
            _dbContext = dbContext;
            _smtpClientCredentials = smtpClientCredentials;
            _emailTemplateHelper = emailTemplateHelper;
            _databaseConnectionHelper = databaseConnectionHelper;
            _logger = logger;
            _hostingSettings = hostingSettings;
        }

        /// <inheritdoc/>
        public async Task SendEmails()
        {
            try
            {
                var users = await _dbContext
                    .Users
                    .Include(e => e.BoardSubscriptions)
                    .Include(e => e.Avatar)
                    .AsNoTracking()
                    .Where(e => e.BoardSubscriptions.Any())
                    .ToListAsync();

                var smtpClient = PrepareSmtpClient();

                foreach (var user in users)
                {
                    var message = await PrepareEmailData(user);
                    smtpClient.SendAsync(message, null);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
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

        private async Task<MailMessage> PrepareEmailData(AppUser user)
        {
            var (body, models) = await PrepareHtmlContentWithModels(user);

            var emailMessage = new MailMessage
            {
                From = new MailAddress("unitedfamilyofficial@gmail.com", "Smashed Potatoes"),
                Subject = "Weekly summary",
                IsBodyHtml = true,
                Body = body,
                Priority = MailPriority.Normal,
            };

            emailMessage.To.Add(new MailAddress(user.Email));

            var alternateView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
            var imageLinks = new List<LinkedResource>();

            models.BoardThreadWithRepliesModels.ForEach(e =>
                imageLinks.Add(new LinkedResource(e.ThreadMainPicturePath, "image/png")
                {
                    ContentId = e.ThreadMainPictureContentId,
                    TransferEncoding = TransferEncoding.Base64
                }));

            imageLinks.ForEach(e => alternateView.LinkedResources.Add(e));

            emailMessage.AlternateViews.Add(alternateView);

            return emailMessage;
        }

        private async Task<Tuple<string, WeeklySummaryModel>> PrepareHtmlContentWithModels(AppUser user)
        {
            try
            {
                using (var connection = _databaseConnectionHelper.CreateConnection())
                {
                    var sql = $@"
select distinct t.id           as ThreadId,
                t.{nameof(PotatoThread.Name).ToSnakeCase()}         as ThreadName,
                b.{nameof(PotatoBoard.Name).ToSnakeCase()} as BoardName,
                (
                    select count(r1.id)
                    from {nameof(BaseDbContext.Replies).ToSnakeCase()} r1
                    where r1.{nameof(PotatoReply.ThreadId).ToSnakeCase()} = t.id
                      and r1.{nameof(PotatoReply.Created).ToSnakeCase()} >= now() - INTERVAL '7 DAYS'
                )              as NumberOfReplies,
                i.{nameof(ImageModel.FileName).ToSnakeCase()}    as FileName
from {nameof(BaseDbContext.Threads).ToSnakeCase()} t
         inner join {nameof(BaseDbContext.Replies).ToSnakeCase()} r
                    on t.id = r.{nameof(PotatoReply.ThreadId).ToSnakeCase()}
         inner join {nameof(BaseDbContext.Boards).ToSnakeCase()} b on b.id = t.{nameof(PotatoThread.BoardId).ToSnakeCase()}
         inner join {nameof(BaseDbContext.Images).ToSnakeCase()} i on t.{nameof(PotatoThread.MainPictureId).ToSnakeCase()} = i.id
         where {nameof(PotatoThread.BoardId).ToSnakeCase()} = ANY(@boardsUserSubscribedTo)
order by NumberOfReplies desc
limit 8;";

                    var modelsResult = await connection.QueryAsync<BoardThreadWithRepliesModel, string, BoardThreadWithRepliesModel>(
                        sql,
                        (model, fileName) =>
                        {
                            model.ThreadMainPicturePath = StoragePathsHelper.GetThreadPictureStoragePath(fileName);
                            model.ThreadMainPictureContentId = Guid.NewGuid().ToString().Replace("-", string.Empty);
                            return model;
                        },
                        new
                        {
                            boardsUserSubscribedTo = user.BoardSubscriptions.Select(e => e.BoardId).ToList()
                        },
                        splitOn: "FileName");

                    var models = modelsResult.ToList();

                    var fullModel = new WeeklySummaryModel
                    {
                        User = user,
                        Domain = _hostingSettings.Value.DomainName,
                        BoardThreadWithRepliesModels = models
                    };

                    var html = await _emailTemplateHelper.GetEmailTemplateAsString("WeeklySummary", fullModel);

                    return new Tuple<string, WeeklySummaryModel>(html, fullModel);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Tuple<string, WeeklySummaryModel>(string.Empty, null);
            }
        }
    }
}