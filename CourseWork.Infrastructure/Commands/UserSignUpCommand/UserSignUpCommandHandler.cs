using System.Threading;
using System.Threading.Tasks;
using CourseWork.Domain.Identity;
using CourseWork.Infrastructure.Database;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CourseWork.Infrastructure.Commands.UserSignUpCommand
{
    /// <summary>
    /// UserSignUpCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{UserSignUpCommand}" />
    public class UserSignUpCommandHandler : IRequestHandler<UserSignUpCommand, ExecutionResult>
    {
        private readonly ILogger<UserSignUpCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSignUpCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userManager">The user manager.</param>
        public UserSignUpCommandHandler(
            ILogger<UserSignUpCommandHandler> logger,
            BaseDbContext dbContext,
            UserManager<AppUser> userManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: UserSignUpCommand</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string</returns>
        public async Task<ExecutionResult> Handle(UserSignUpCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                var newUser = new AppUser
                {
                    Email = request.Email,
                    UserName = request.Username,
                };

                await _userManager.CreateAsync(newUser, request.Password);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new ExecutionResult(new InfoMessage("You have successfully signed up!"));
            }
        }
    }
}