using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Database;
using CourseWork.Core.Models.Board;
using CourseWork.Core.Services.UserService;
using JetBrains.Annotations;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Queries.Board.GetAllBoards
{
    /// <summary>
    /// GetAllBoardsQuery handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{GetAllBoardsQuery}" />
    [UsedImplicitly]
    public class GetAllBoardsQueryHandler : IRequestHandler<GetAllBoardsQuery,
        ExecutionResult<GetAllBoardsQueryResult>>
    {
        private readonly ILogger<GetAllBoardsQueryHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllBoardsQueryHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userService">The user service.</param>
        public GetAllBoardsQueryHandler(
            ILogger<GetAllBoardsQueryHandler> logger,
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
        /// <param name="request">The request: GetAllBoardsQuery.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult<GetAllBoardsQueryResult>> Handle(
            GetAllBoardsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();

                if (user is null)
                {
                    return new ExecutionResult<GetAllBoardsQueryResult>(new ErrorInfo("The user is not authorized"));
                }

                var boardModels = await _dbContext
                    .Boards
                    .AsNoTracking()
                    .Select(f => new BoardModel
                    {
                        Id = f.Id,
                        Name = f.Name,
                        DisplayName = f.DisplayName
                    })
                    .ToListAsync(cancellationToken);

                var result = new GetAllBoardsQueryResult { Models = boardModels };

                return new ExecutionResult<GetAllBoardsQueryResult>(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult<GetAllBoardsQueryResult>(
                    new ErrorInfo($"Error while executing {nameof(GetAllBoardsQuery)}"));
            }
        }
    }
}