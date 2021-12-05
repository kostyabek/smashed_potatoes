using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Common.Consts;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities.Admin;
using CourseWork.Core.Services.UserService;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Commands.Admin.BanUser
{
    /// <summary>
    /// BanUserCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{BanUserCommand}" />
    public class BanUserCommandHandler : IRequestHandler<BanUserCommand, ExecutionResult>
    {
        private readonly ILogger<BanUserCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BanUserCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userService">The user service.</param>
        public BanUserCommandHandler(
            ILogger<BanUserCommandHandler> logger,
            BaseDbContext dbContext,
            IUserService userService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userService = userService;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: BanUserCommand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currentUserId = _userService.UserId;

                var userToBan = await _dbContext
                    .Users
                    .Include(e => e.UserRoles)
                    .SingleOrDefaultAsync(e => e.Id == request.UserId, cancellationToken);

                if (userToBan is null)
                {
                    return new ExecutionResult(new ErrorInfo("No such user has been found."));
                }

                if (userToBan.Id == currentUserId)
                {
                    return new ExecutionResult(new ErrorInfo("You cannot ban yourself."));
                }

                if (userToBan.UserRoles.Any(e => e.RoleId == AppConsts.UserRoles.Admin))
                {
                    return new ExecutionResult(new ErrorInfo("Admins cannot be banned."));
                }

                var banRecord = new Ban
                {
                    UserId = request.UserId,
                    Reason = request.Reason,
                    IsPermanent = true,
                    IsActive = true
                };

                if (!request.IsBanPermanent)
                {
                    banRecord.IsPermanent = false;
                    banRecord.Until = request.BannedUntil;
                }

                _dbContext.Bans.Add(banRecord);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new ExecutionResult(new InfoMessage("The user has been banned successfully."));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult(
                    new ErrorInfo($"Error while executing {nameof(BanUserCommandHandler)}"));
            }
        }
    }
}