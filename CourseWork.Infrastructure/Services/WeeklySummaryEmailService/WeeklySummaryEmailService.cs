namespace CourseWork.Core.Services.WeeklySummaryEmailService
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Common.Configurations;
    using Dapper;
    using Database;
    using Database.DatabaseConnectionHelper;
    using Database.Entities.Boards;
    using Database.Entities.Files;
    using Database.Entities.Identity;
    using Database.Entities.Replies;
    using Database.Entities.Threads;
    using Helpers;
    using Helpers.EmailTemplateHelper;
    using LS.Helpers.Hosting.Extensions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Models.EmailTemplate;
    using Models.Reply;

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
        private readonly IWebHostBuilder _webHostBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeeklySummaryEmailService" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="smtpClientCredentials">The SMTP client credentials.</param>
        /// <param name="emailTemplateHelper">The en email template helper.</param>
        /// <param name="databaseConnectionHelper">The database connection helper.</param>
        /// <param name="webHostBuilder">The web host builder.</param>
        public WeeklySummaryEmailService(BaseDbContext dbContext,
            IOptions<SmtpClientCredentials> smtpClientCredentials,
            IEmailTemplateHelper emailTemplateHelper,
            IDatabaseConnectionHelper databaseConnectionHelper,
            IWebHostBuilder webHostBuilder)
        {
            _dbContext = dbContext;
            _smtpClientCredentials = smtpClientCredentials;
            _emailTemplateHelper = emailTemplateHelper;
            _databaseConnectionHelper = databaseConnectionHelper;
            _webHostBuilder = webHostBuilder;
        }

        /// <inheritdoc/>
        public async Task SendEmails()
        {
            var users = await _dbContext
                .Users
                .Include(e => e.BoardSubscriptions)
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
            var body = await PrepareHtmlContent(user);
            var emailMessage = new MailMessage
            {
                From = new MailAddress("unitedfamilyofficial@gmail.com", "Smashed Potatoes"),
                Subject = "Weekly summary",
                IsBodyHtml = true,
                Body = body
            };

            emailMessage.To.Add(new MailAddress(user.Email));
            return emailMessage;
        }

        private async Task<string> PrepareHtmlContent(AppUser user)
        {
            try
            {
                using (var connection = _databaseConnectionHelper.CreateConnection())
                {
                    var sql = $@"
select distinct t.id           as ThreadId,
                t.{nameof(PotatoThread.Name).ToSnakeCase()}         as ThreadName,
                b.{nameof(PotatoBoard.DisplayName).ToSnakeCase()} as BoardName,
                i.{nameof(ImageModel.FileName).ToSnakeCase()}    as FileName,
                (
                    select count(r1.id)
                    from {nameof(BaseDbContext.Replies).ToSnakeCase()} r1
                    where r1.{nameof(PotatoReply.ThreadId).ToSnakeCase()} = t.id
                      and r1.{nameof(PotatoReply.Created).ToSnakeCase()} >= now() - INTERVAL '24 HOURS'
                )              as NumberOfReplies
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
                            model.ThreadMainPicturePath = StoragePathsHelper.GetRelatedPictureStoragePath(fileName);
                            return model;
                        },
                        new
                        {
                            boardsUserSubscribedTo = user.BoardSubscriptions.Select(e => e.BoardId).ToList()
                        },
                        splitOn: "FileName");

                    var models = modelsResult.ToList();
                    var shit = _webHostBuilder.GetSetting(WebHostDefaults.ServerUrlsKey);

                    foreach (var model in models)
                    {
                        model.Replies = await _dbContext
                            .Replies
                            .Include(e => e.User)
                            .Include(e => e.PicRelated)
                            .AsNoTracking()
                            .Where(e => e.IsThreadStarter == false && e.ThreadId == model.ThreadId)
                            .OrderByDescending(e => e.Created)
                            .Select(e => new ReplyListModel
                            {
                                Content = e.Content,
                                UserDisplayName = e.User.DisplayName,
                                PicRelatedPath = e.PicRelated == null ? null : _webHostBuilder.GetSetting(WebHostDefaults.ServerUrlsKey)
                            })
                            .ToListAsync();
                    }

                    var fullModel = new WeeklySummaryModel
                    {
                        User = user,
                        BoardThreadWithRepliesModels = models
                    };

                    return await _emailTemplateHelper.GetEmailTemplateAsString("WeeklySummary", fullModel);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return string.Empty;
            }
        }
    }
}
