namespace CourseWork.Core.Commands.Reply.CreateNewReply
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities.Files;
    using Database.Entities.Replies;
    using Helpers;
    using LS.Helpers.Hosting.API;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Services.UserService;

    /// <summary>
    /// CreateNewReplyCommand handler.
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{CreateNewReplyCommand}" />
    public class CreateNewReplyCommandHandler : IRequestHandler<CreateNewReplyCommand, ExecutionResult>
    {
        private readonly ILogger<CreateNewReplyCommandHandler> _logger;
        private readonly BaseDbContext _dbContext;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewReplyCommandHandler" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="userService">The user service.</param>
        public CreateNewReplyCommandHandler(
            ILogger<CreateNewReplyCommandHandler> logger,
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
        /// <param name="request">The request: CreateNewReplyCommand</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>string</returns>
        public async Task<ExecutionResult> Handle(CreateNewReplyCommand request,
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

                    if (!request.RepliedToIds.Any())
                    {
                        return new ExecutionResult(
                            new ErrorInfo("Every reply must refer to at least one post on the thread!"));
                    }

                    var newReplyRecord = new PotatoReply
                    {
                        UserId = user.Id,
                        ThreadId = request.ThreadId,
                        Created = DateTime.UtcNow,
                        Content = request.Content,
                        IsThreadStarter = false
                    };

                    if (request.PicRelated != null)
                    {
                        var fileNameBuilder = new StringBuilder(Path.GetFileNameWithoutExtension(request.PicRelated.FileName).Replace(' ', '-'));
                        fileNameBuilder.Append(DateTime.UtcNow.ToString("yymmssfff"));
                        fileNameBuilder.Append(Path.GetExtension(request.PicRelated.FileName));
                        var fileName = fileNameBuilder.ToString();

                        var filePath = StoragePathsHelper.GetRelatedPictureStoragePath(fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await request.PicRelated.CopyToAsync(fileStream, cancellationToken);
                        }

                        var pictureDbRecord = new ImageModel
                        {
                            FileName = fileName
                        };

                        _dbContext.Images.Add(pictureDbRecord);
                        await _dbContext.SaveChangesAsync(cancellationToken);

                        newReplyRecord.PicRelatedId = pictureDbRecord.Id;
                    }

                    _dbContext.Replies.Add(newReplyRecord);

                    newReplyRecord.ReplyReplies = new List<ReplyReply>();
                    foreach (var repliedToId in request.RepliedToIds)
                    {
                        newReplyRecord.ReplyReplies.Add(new ReplyReply
                        {
                            PointingReplyId = newReplyRecord.Id,
                            PointedReplyId = repliedToId
                        });
                    }

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    return new ExecutionResult(new InfoMessage("Reply has been created successfully."));
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError(e.Message);
                    return new ExecutionResult(
                        new ErrorInfo($"Error while executing {nameof(CreateNewReplyCommand)}"));
                }
            }
        }
    }
}