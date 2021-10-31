namespace CourseWork.Infrastructure.Commands.UserSignInCommand
{
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using LS.Helpers.Hosting.API;
    using MediatR;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// UserSignInCommand handler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{UserSignInCommand}" />
    public class UserSignInCommandHandler : IRequestHandler<UserSignInCommand, ExecutionResult>
    {
        private readonly ILogger<UserSignInCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSignInCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        public UserSignInCommandHandler(
            ILogger<UserSignInCommandHandler> logger,
            BaseDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: UserSignInCommand</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string</returns>
        public async Task<ExecutionResult> Handle(UserSignInCommand request, CancellationToken cancellationToken)
        {
            return new ExecutionResult();
        }
    }
}