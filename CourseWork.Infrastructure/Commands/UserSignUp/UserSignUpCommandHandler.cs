using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Common.Consts;
using CourseWork.Core.Commands.UserSignIn;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities.Identity;
using CourseWork.Core.Helpers;
using CourseWork.Core.Models.Auth;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using CourseWork.Core.Database.Entities;

namespace CourseWork.Core.Commands.UserSignUp
{
    /// <summary>
    /// UserSignUpCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{UserSignUpCommand}" />
    public class UserSignUpCommandHandler : IRequestHandler<UserSignUpCommand, ExecutionResult<SignedInUser>>
    {
        private readonly ILogger<UserSignUpCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSignUpCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="mediator">Mediator.</param>
        public UserSignUpCommandHandler(
            ILogger<UserSignUpCommandHandler> logger,
            BaseDbContext dbContext,
            UserManager<AppUser> userManager,
            IMediator mediator)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
            _mediator = mediator;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request: UserSignUpCommand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult<SignedInUser>> Handle(UserSignUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
                {
                    var fileName = Path.GetFileName(request.Avatar.FileName);
                    var filePath = StoragePathsHelper.GetAvatarStoragePath(fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.Avatar.CopyToAsync(fileStream, cancellationToken);
                    }

                    var avatarDbRecord = new ImageModel
                    {
                        FilePath = filePath
                    };

                    _dbContext.Images.Add(avatarDbRecord);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    var newUser = new AppUser
                    {
                        Email = request.Email,
                        UserName = request.Username,
                        AvatarId = avatarDbRecord.Id,
                    };

                    await _userManager.CreateAsync(newUser, request.Password);

                    newUser.UserRoles.Add(new AppUserRole
                    {
                        UserId = newUser.Id,
                        RoleId = AppConsts.UserRoles.NewUser
                    });

                    var signInCommand = new UserSignInCommand
                    {
                        Username = request.Username,
                        Password = request.Password
                    };

                    var signInResult = await _mediator.Send(signInCommand, cancellationToken);

                    if (!signInResult.Success)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return new ExecutionResult<SignedInUser>(signInResult);
                    }

                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return new ExecutionResult<SignedInUser>(new InfoMessage("You have successfully signed up!"));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ExecutionResult<SignedInUser>(
                    new ErrorInfo($"Error while executing {nameof(UserSignUpCommandHandler)}"));
            }
        }
    }
}