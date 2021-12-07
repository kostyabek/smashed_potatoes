using System;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Database;
using CourseWork.Core.Services.UserService;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Commands.Profile.EditDisplayName
{
    /// <summary>
    /// EditDisplayNameCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{EditGeneralInfoCommand}" />
    public class EditDisplayNameCommandHandler : IRequestHandler<EditDisplayNameCommand, ExecutionResult>
    {
        private readonly ILogger<EditDisplayNameCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditDisplayNameCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userService">The user service.</param>
        public EditDisplayNameCommandHandler(
            ILogger<EditDisplayNameCommandHandler> logger,
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
        /// <param name="request">The request: EditGeneralInfoCommand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult> Handle(EditDisplayNameCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();

                user.DisplayName = request.DisplayName;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new ExecutionResult(new InfoMessage("Edits have been saved successfully."));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult(
                    new ErrorInfo($"Error while executing {nameof(EditDisplayNameCommandHandler)}"));
            }
        }
    }
}