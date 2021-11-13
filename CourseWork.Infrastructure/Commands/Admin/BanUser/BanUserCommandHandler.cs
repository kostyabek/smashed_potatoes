namespace CourseWork.Core.Commands.Admin.BanUser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities.Admin;
    using LS.Helpers.Hosting.API;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// BanUserCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{BanUserCommand}" />
    public class BanUserCommandHandler : IRequestHandler<BanUserCommand, ExecutionResult>
    {
        private readonly ILogger<BanUserCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BanUserCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public BanUserCommandHandler(
            ILogger<BanUserCommandHandler> logger,
            BaseDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: BanUserCommand</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userToBan = await _dbContext
                    .Users
                    .SingleOrDefaultAsync(e => e.Id == request.UserId, cancellationToken);

                if (userToBan is null)
                {
                    return new ExecutionResult(new ErrorInfo("No such user has been found."));
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
                    if (!request.BannedUntil.HasValue)
                    {
                        return new ExecutionResult(new ErrorInfo("Non-permanent bans must have due date and time."));
                    }

                    if ((int)(request.BannedUntil.Value - DateTime.UtcNow).TotalMinutes < 5)
                    {
                        return new ExecutionResult(new ErrorInfo("Ban timespan must be at least 5 minutes."));
                    }

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