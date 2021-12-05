using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Common.Consts;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities.Boards;
using CourseWork.Core.Database.Entities.Files;
using CourseWork.Core.Database.Entities.Replies;
using CourseWork.Core.Database.Entities.Threads;
using CourseWork.Core.Helpers;
using CourseWork.Core.Helpers.DatabaseConnectionHelper;
using CourseWork.Core.Models.Thread;
using CourseWork.Core.Services.UserService;
using Dapper;
using JetBrains.Annotations;
using LS.Helpers.Hosting.API;
using LS.Helpers.Hosting.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Queries.Thread.GetPopularThreads
{
    /// <summary>
    /// GetPopularThreadsQuery handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{GetPopularThreadsQuery}" />
    [UsedImplicitly]
    public class GetPopularThreadsQueryHandler : IRequestHandler<GetPopularThreadsQuery,
        ExecutionResult<GetPopularThreadsQueryResult>>
    {
        private readonly ILogger<GetPopularThreadsQueryHandler> _logger;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDatabaseConnectionHelper _databaseConnectionHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPopularThreadsQueryHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="databaseConnectionHelper">The database connection helper.</param>
        public GetPopularThreadsQueryHandler(
            ILogger<GetPopularThreadsQueryHandler> logger,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor,
            IDatabaseConnectionHelper databaseConnectionHelper)
        {
            _logger = logger;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _databaseConnectionHelper = databaseConnectionHelper;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: GetPopularThreadsQuery.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult<GetPopularThreadsQueryResult>> Handle(
            GetPopularThreadsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();

                if (user is null)
                {
                    return new ExecutionResult<GetPopularThreadsQueryResult>(new ErrorInfo("The user is not authorized"));
                }

                var httpRequest = _httpContextAccessor.HttpContext.Request;
                var mainPicturePath = StoragePathsHelper
                    .GetImagesStaticFilesPath(httpRequest)
                    .ToString();

                var sql = $@"
select distinct t.id           as Id,
                t.{nameof(PotatoThread.Name).ToSnakeCase()}         as Header,
                b.{nameof(PotatoBoard.Name).ToSnakeCase()}         as BoardName,
                b.{nameof(PotatoBoard.DisplayName).ToSnakeCase()} as BoardDisplayName,
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
order by NumberOfReplies desc
limit 8;";

                using (var connection = _databaseConnectionHelper.CreateConnection())
                {
                    var modelsResult = await connection.QueryAsync<PopularThreadModel, string, PopularThreadModel>(
                        sql,
                        (model, fileName) =>
                        {
                            var imagePathBuilder = new StringBuilder(mainPicturePath);
                            model.MainPicturePath = imagePathBuilder
                                .Append($"{AppConsts.StoragePaths.ThreadPics}/")
                                .Append(fileName)
                                .ToString();

                            return model;
                        },
                        splitOn: "FileName");

                    var models = modelsResult.ToList();
                    var result = new GetPopularThreadsQueryResult { Models = models };

                    return new ExecutionResult<GetPopularThreadsQueryResult>(result);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult<GetPopularThreadsQueryResult>(
                    new ErrorInfo($"Error while executing {nameof(GetPopularThreadsQuery)}"));
            }
        }
    }
}