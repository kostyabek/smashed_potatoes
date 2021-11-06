using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Common.Consts;
using CourseWork.Core.Commands.Auth.UserSignIn;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities;
using CourseWork.Core.Database.Entities.Identity;
using CourseWork.Core.Helpers;
using CourseWork.Core.Models.Auth;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Commands.Auth.UserSignUp
{
    using System.Text;
    using Database.Entities.Files;

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
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var newUser = new AppUser
                    {
                        Email = request.Email,
                        UserName = request.Username,
                        DisplayName = request.DisplayName,
                    };

                    await _userManager.CreateAsync(newUser, request.Password);

                    newUser.UserRoles = new List<AppUserRole>
                    {
                        new ()
                        {
                            UserId = newUser.Id,
                            RoleId = AppConsts.UserRoles.NewUser
                        }
                    };

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    if (request.Avatar != null)
                    {
                        var fileNameBuilder = new StringBuilder(Path.GetFileNameWithoutExtension(request.Avatar.FileName).Replace(' ', '-'));
                        fileNameBuilder.Append(DateTime.UtcNow.ToString("yymmssfff"));
                        fileNameBuilder.Append(Path.GetExtension(request.Avatar.FileName));
                        var fileName = fileNameBuilder.ToString();

                        var filePath = StoragePathsHelper.GetAvatarStoragePath(fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await request.Avatar.CopyToAsync(fileStream, cancellationToken);
                        }

                        var avatarDbRecord = new ImageModel
                        {
                            FileName = fileName
                        };

                        _dbContext.Images.Add(avatarDbRecord);
                        await _dbContext.SaveChangesAsync(cancellationToken);

                        newUser.AvatarId = avatarDbRecord.Id;
                    }

                    await transaction.CommitAsync(cancellationToken);

                    var signInCommand = new UserSignInCommand
                    {
                        Username = request.Username,
                        Password = request.Password
                    };

                    var signInResult = await _mediator.Send(signInCommand, cancellationToken);

                    if (!signInResult.Success)
                    {
                        return new ExecutionResult<SignedInUser>(signInResult);
                    }

                    return new ExecutionResult<SignedInUser>(new InfoMessage("You have successfully signed up!"));
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    await transaction.RollbackAsync(cancellationToken);
                    return new ExecutionResult<SignedInUser>(
                        new ErrorInfo($"Error while executing {nameof(UserSignUpCommandHandler)}"));
                }
            }
        }
    }
}