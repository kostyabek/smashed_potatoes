using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Common.Consts;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities.Admin;
using CourseWork.Core.Helpers;
using CourseWork.Core.Models.Admin;
using JetBrains.Annotations;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Queries.Admin.GetBannedUsers
{
    /// <summary>
    /// GetBannedUsersQuery handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{GetBannedUsersQuery}" />
    [UsedImplicitly]
    public class GetBannedUsersQueryHandler : IRequestHandler<GetBannedUsersQuery,
        ExecutionResult<GetBannedUsersQueryResult>>
    {
        private readonly ILogger<GetBannedUsersQueryHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetBannedUsersQueryHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public GetBannedUsersQueryHandler(
            ILogger<GetBannedUsersQueryHandler> logger,
            BaseDbContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: GetBannedUsersQuery.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult<GetBannedUsersQueryResult>> Handle(
            GetBannedUsersQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<Ban, bool>> displayNameFilter = !string.IsNullOrWhiteSpace(request.DisplayName) ? e => e.User.DisplayName.Contains(request.DisplayName.Trim()) : e => true;
                Expression<Func<Ban, bool>> emailFilter = !string.IsNullOrWhiteSpace(request.Email) ? e => e.User.Email.Contains(request.Email.Trim()) : e => true;
                Expression<Func<Ban, bool>> banEndDateFilter = request.BanEndDate.HasValue ? e => e.Until.HasValue && e.Until.Value.Date == request.BanEndDate.Value : e => true;

                var httpRequest = _httpContextAccessor.HttpContext.Request;
                var imagesPathBuilder = StoragePathsHelper.GetImagesStaticFilesPath(httpRequest);
                var imagesPath = imagesPathBuilder.ToString();

                var offset = request.PageSize * (request.PageNumber - 1);
                var limit = request.PageSize;

                var bannedUsers = await _dbContext
                    .Bans
                    .Include(e => e.User)
                    .ThenInclude(e => e.Avatar)
                    .AsNoTracking()
                    .Where(displayNameFilter)
                    .Where(banEndDateFilter)
                    .Where(emailFilter)
                    .Skip(offset)
                    .Take(limit)
                    .Select(e => new BannedUserListItemModel
                    {
                        UserId = e.UserId,
                        AvatarPath = e.User.Avatar == null ? null : $"{imagesPath}{AppConsts.StoragePaths.Avatars}/{e.User.Avatar.FileName}",
                        BannedUntil = e.Until,
                        Reason = e.Reason,
                        IsBanPermanent = e.IsPermanent,
                        DisplayName = e.User.DisplayName,
                        Email = e.User.Email
                    })
                    .ToListAsync(cancellationToken);

                var result = new GetBannedUsersQueryResult { Models = bannedUsers };

                return new ExecutionResult<GetBannedUsersQueryResult>(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult<GetBannedUsersQueryResult>(
                    new ErrorInfo($"Error while executing {nameof(GetBannedUsersQuery)}"));
            }
        }
    }
}