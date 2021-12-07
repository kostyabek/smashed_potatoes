using System.Threading.Tasks;
using CourseWork.Core.Services.WeeklySummaryEmailService;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CourseWork.Core.Quartz.Jobs
{
    /// <summary>
    /// Weekly Summary Email Job.
    /// </summary>
    /// <seealso cref="IJob" />
    public class WeeklySummaryEmailJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeeklySummaryEmailJob"/> class.
        /// </summary>
        /// <param name="serviceScopeFactory">The service scope factory.</param>
        public WeeklySummaryEmailJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <inheritdoc/>
        public async Task Execute(IJobExecutionContext context)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetService<IWeeklySummaryEmailService>();
            await service.SendEmails();
        }
    }
}
