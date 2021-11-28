namespace CourseWork.Core.Commands.Profile.SetBoardsEmailSubscription
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities.Profile;
    using LS.Helpers.Hosting.API;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Services.UserService;

    /// <summary>
    /// SetBoardsEmailSubscriptionCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{SetBoardsEmailSubscriptionCommand}" />
    public class
        SetBoardsEmailSubscriptionCommandHandler : IRequestHandler<SetBoardsEmailSubscriptionCommand,
            ExecutionResult>
    {
        private readonly ILogger<SetBoardsEmailSubscriptionCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetBoardsEmailSubscriptionCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userService">The user service.</param>
        public SetBoardsEmailSubscriptionCommandHandler(
            ILogger<SetBoardsEmailSubscriptionCommandHandler> logger,
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
        /// <param name="request">The request: SetBoardsEmailSubscriptionCommand</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string</returns>
        public async Task<ExecutionResult> Handle(SetBoardsEmailSubscriptionCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var userId = _userService.UserId;
                var user = await _dbContext
                    .Users
                    .Include(e => e.BoardSubscriptions)
                    .SingleOrDefaultAsync(e => e.Id == userId, cancellationToken);

                var existingBoardIds = await _dbContext
                    .Boards
                    .AsNoTracking()
                    .Select(e => e.Id)
                    .ToListAsync(cancellationToken);

                if (user.BoardSubscriptions.Count + request.BoardIds.Count > 3)
                {
                    return new ExecutionResult(new ErrorInfo("Only 3 or less board subscriptions are allowed."));
                }

                if (!request.BoardIds.All(i => existingBoardIds.Contains(i)))
                {
                    return new ExecutionResult(new ErrorInfo("Some of the boards provided do not exist."));
                }

                var userBoardSubscriptions = new List<UserBoardSubscription>();
                request
                    .BoardIds
                    .ForEach(boardId => userBoardSubscriptions
                        .Add(new UserBoardSubscription
                        {
                            UserId = userId,
                            BoardId = boardId
                        }));

                user.BoardSubscriptions = userBoardSubscriptions;
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new ExecutionResult(new InfoMessage("Subscription has been set successfully."));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult(
                    new ErrorInfo($"Error while executing {nameof(SetBoardsEmailSubscriptionCommandHandler)}"));
            }
        }
    }
}