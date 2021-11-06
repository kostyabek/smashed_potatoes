using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities.Files;
using CourseWork.Core.Helpers;
using CourseWork.Core.Services.UserService;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Commands.Profile.ChangeAvatar
{
    /// <summary>
    /// ChangeAvatarCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{ChangeAvatarCommand}" />
    public class ChangeAvatarCommandHandler : IRequestHandler<ChangeAvatarCommand, ExecutionResult>
    {
        private readonly ILogger<ChangeAvatarCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeAvatarCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userService">The user service.</param>
        public ChangeAvatarCommandHandler(
            ILogger<ChangeAvatarCommandHandler> logger,
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
        /// <param name="request">The request: ChangeAvatarCommand</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// string.
        /// </returns>
        public async Task<ExecutionResult> Handle(ChangeAvatarCommand request,
            CancellationToken cancellationToken)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var user = await _userService.GetCurrentUserAsync();

                    if (user is null)
                    {
                        return new ExecutionResult(new ErrorInfo("The user is not authorized"));
                    }

                    var fileNameBuilder = new StringBuilder(Path.GetFileNameWithoutExtension(request.Avatar.FileName).Replace(' ', '-'));
                    fileNameBuilder.Append(DateTime.UtcNow.ToString("yymmssfff"));
                    fileNameBuilder.Append(Path.GetExtension(request.Avatar.FileName));
                    var fileName = fileNameBuilder.ToString();

                    var avatarDbRecord = new ImageModel
                    {
                        FileName = fileName,
                        Created = DateTime.UtcNow
                    };

                    _dbContext.Images.Add(avatarDbRecord);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    user.AvatarId = avatarDbRecord.Id;
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    var filePath = StoragePathsHelper.GetAvatarStoragePath(fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.Avatar.CopyToAsync(fileStream, cancellationToken);
                    }

                    await transaction.CommitAsync(cancellationToken);
                    return new ExecutionResult(new InfoMessage("New avatar has been set successfully."));
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError(e.Message);
                    return new ExecutionResult(
                        new ErrorInfo($"Error while executing {nameof(ChangeAvatarCommandHandler)}"));
                }
            }
        }
    }
}