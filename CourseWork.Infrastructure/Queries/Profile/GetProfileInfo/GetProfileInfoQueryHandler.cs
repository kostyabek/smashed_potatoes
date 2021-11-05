using System;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Database;
using JetBrains.Annotations;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Queries.Profile.GetProfileInfo
{
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Services.UserService;

    /// <summary>
    /// GetProfileInfoQuery handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{GetProfileInfoQuery}" />
    [UsedImplicitly]
    public class GetProfileInfoQueryHandler : IRequestHandler<GetProfileInfoQuery,
        ExecutionResult<GetProfileInfoQueryResult>>
    {
        private readonly ILogger<GetProfileInfoQueryHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfileInfoQueryHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="httpContext">The HTTP context.</param>
        public GetProfileInfoQueryHandler(
            ILogger<GetProfileInfoQueryHandler> logger,
            BaseDbContext dbContext,
            IUserService userService,
            IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userService = userService;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: GetProfileInfoQuery.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult<GetProfileInfoQueryResult>> Handle(
            GetProfileInfoQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var userId = _userService.UserId;

                var user = await _dbContext
                    .Users
                    .AsNoTracking()
                    .Include(u => u.Avatar)
                    .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);

                if (user is null)
                {
                    return new ExecutionResult<GetProfileInfoQueryResult>(new ErrorInfo("The user is not authorized"));
                }

                var httpRequest = _httpContext.HttpContext.Request;
                var result = new GetProfileInfoQueryResult
                {
                    Id = userId,
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    AvatarPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/images/{user.Avatar.FileName}",
                };

                return new ExecutionResult<GetProfileInfoQueryResult>(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult<GetProfileInfoQueryResult>(
                    new ErrorInfo($"Error while executing {nameof(GetProfileInfoQuery)}"));
            }
        }
    }
}