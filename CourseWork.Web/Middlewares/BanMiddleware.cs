using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CourseWork.Core.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Web.Middlewares
{
    /// <summary>
    /// Ban middleware.
    /// </summary>
    public class BanMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="BanMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public BanMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userService">The user service.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            var userIdString = context
                .User
                .Claims
                .FirstOrDefault(e => e.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", StringComparison.CurrentCultureIgnoreCase))
                .Value;

            if (!int.TryParse(userIdString, out var userId))
            {
                await _next(context);
            }

            var isBanned = await userService.IsGivenUserBanned(userId);

            if (isBanned)
            {
                var problem = new ProblemDetails
                {
                    Instance = context.Request.Path,
                    Title = "Access denied",
                    Detail = "The user is banned",
                    Status = StatusCodes.Status401Unauthorized,
                };
                context.Response.StatusCode = problem.Status.Value;

                await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
                return;
            }

            await _next(context);
        }
    }
}
