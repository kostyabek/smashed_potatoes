using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities.Identity;
using CourseWork.Core.Extensions;
using CourseWork.Core.Models.Auth;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Commands.Auth.UserSignIn
{
    /// <summary>
    /// UserSignInCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{UserSignInCommand}" />
    public class UserSignInCommandHandler : IRequestHandler<UserSignInCommand, ExecutionResult<SignedInUser>>
    {
        private readonly ILogger<UserSignInCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly SignInManager<AppUser> _signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSignInCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public UserSignInCommandHandler(
            ILogger<UserSignInCommandHandler> logger,
            BaseDbContext dbContext,
            SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: UserSignInCommand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult<SignedInUser>> Handle(UserSignInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userInitial = await _dbContext
                    .Users
                    .Include(e => e.Bans)
                    .SingleOrDefaultAsync(e => e.UserName == request.Username, cancellationToken);

                if (userInitial is null)
                {
                    return new ExecutionResult<SignedInUser>(new ErrorInfo("No such user found!"));
                }

                var signInResult = await _signInManager.PasswordSignInWithBanCheckAsync(userInitial, request.Password, true, false);

                if (signInResult.IsNotAllowed)
                {
                    return new ExecutionResult<SignedInUser>(new ErrorInfo("The user is banned."));
                }

                if (!signInResult.Succeeded)
                {
                    return new ExecutionResult<SignedInUser>(new ErrorInfo("Invalid login attempt."));
                }

                var user = await _dbContext
                    .Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(r => r.Role)
                    .SingleOrDefaultAsync(u => u.Id == userInitial.Id, cancellationToken);

                var result = new SignedInUser
                {
                    Id = user.Id,
                    Roles = user
                        .UserRoles
                        .Select(r => r.Role.Name)
                        .ToList()
                };

                return new ExecutionResult<SignedInUser>(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult<SignedInUser>(
                    new ErrorInfo($"Error while executing {nameof(UserSignInCommandHandler)}"));
            }
        }
    }
}