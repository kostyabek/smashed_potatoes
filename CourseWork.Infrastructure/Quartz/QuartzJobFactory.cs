namespace CourseWork.Core.Quartz
{
    using global::Quartz;
    using global::Quartz.Spi;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Quartz Job Factory.
    /// </summary>
    /// <seealso cref="Quartz.Spi.IJobFactory" />
    public class QuartzJobFactory : IJobFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuartzJobFactory"/> class.
        /// </summary>
        /// <param name="serviceScopeFactory">The service scope factory.</param>
        public QuartzJobFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <inheritdoc/>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var job = scope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
            return job;
        }

        /// <inheritdoc/>
        public void ReturnJob(IJob job)
        {
        }
    }
}
