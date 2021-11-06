using System;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities.Boards;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.Extensions.Logging;
using CourseWork.Core.Services.UserService;

namespace CourseWork.Core.Commands.Board.CreateNewBoard
{
    /// <summary>
    /// CreateNewBoardCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{CreateNewBoardCommand}" />
    public class CreateNewBoardCommandHandler : IRequestHandler<CreateNewBoardCommand, ExecutionResult>
    {
        private readonly ILogger<CreateNewBoardCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewBoardCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userService">The user service.</param>
        public CreateNewBoardCommandHandler(
            ILogger<CreateNewBoardCommandHandler> logger,
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
        /// <param name="request">The request: CreateNewBoardCommand</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string</returns>
        public async Task<ExecutionResult> Handle(CreateNewBoardCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();

                if (user is null)
                {
                    return new ExecutionResult(new ErrorInfo("The user is not authorized."));
                }

                var newBoardRecord = new PotatoBoard
                {
                    Name = request.Name,
                    DisplayName = request.DisplayName,
                    Created = DateTime.UtcNow
                };

                _dbContext.Boards.Add(newBoardRecord);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new ExecutionResult(new InfoMessage("New board has been created successfully!"));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult(
                    new ErrorInfo($"Error while executing {nameof(CreateNewBoardCommandHandler)}"));
            }
        }
    }
}