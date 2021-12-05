using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities.Files;
using CourseWork.Core.Database.Entities.Replies;
using CourseWork.Core.Database.Entities.Threads;
using CourseWork.Core.Helpers;
using CourseWork.Core.Services.UserService;
using LS.Helpers.Hosting.API;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseWork.Core.Commands.Thread.CreateNewThread
{
    /// <summary>
    /// CreateNewThreadCommand handler.
    /// </summary>
    /// <seealso cref="IRequestHandler{CreateNewThreadCommand}" />
    public class CreateNewThreadCommandHandler : IRequestHandler<CreateNewThreadCommand, ExecutionResult>
    {
        private readonly ILogger<CreateNewThreadCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewThreadCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userService">The user service.</param>
        public CreateNewThreadCommandHandler(
            ILogger<CreateNewThreadCommandHandler> logger,
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
        /// <param name="request">The request: CreateNewThreadCommand.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string.</returns>
        public async Task<ExecutionResult> Handle(CreateNewThreadCommand request,
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

                    var newThreadRecord = new PotatoThread
                    {
                        BoardId = request.BoardId,
                        Created = DateTime.UtcNow,
                        Description = request.Description,
                        Name = request.Name,
                        UserId = user.Id,
                    };

                    if (request.MainPicture is null)
                    {
                        return new ExecutionResult(new ErrorInfo("Every thread must have a main picture."));
                    }

                    var fileNameBuilder = new StringBuilder(Path.GetFileNameWithoutExtension(request.MainPicture.FileName).Replace(' ', '-'));
                    fileNameBuilder.Append(DateTime.UtcNow.ToString("yymmssfff"));
                    fileNameBuilder.Append(Path.GetExtension(request.MainPicture.FileName));
                    var fileName = fileNameBuilder.ToString();

                    var filePath = StoragePathsHelper.GetThreadPictureStoragePath(fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.MainPicture.CopyToAsync(fileStream, cancellationToken);
                    }

                    var pictureDbRecord = new ImageModel
                    {
                        FileName = fileName
                    };

                    _dbContext.Images.Add(pictureDbRecord);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    newThreadRecord.MainPictureId = pictureDbRecord.Id;

                    _dbContext.Threads.Add(newThreadRecord);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    _dbContext.Replies.Add(new PotatoReply
                    {
                        UserId = user.Id,
                        ThreadId = newThreadRecord.Id,
                        IsThreadStarter = true,
                        Created = newThreadRecord.Created,
                    });
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    return new ExecutionResult(new InfoMessage("Thread has been created successfully."));
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError(e.Message);
                    return new ExecutionResult(
                        new ErrorInfo($"Error while executing {nameof(CreateNewThreadCommandHandler)}"));
                }
            }
        }
    }
}