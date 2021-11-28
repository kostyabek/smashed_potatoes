namespace CourseWork.Core.Quartz
{
    using System;
    using System.Threading.Tasks;
    using global::Quartz;
    using global::Quartz.Impl;
    using global::Quartz.Spi;
    using Jobs;

    /// <summary>
    /// Scheduler Initiator for Quartz.
    /// </summary>
    public static class QuartzSchedulerInitiator
    {
        /// <summary>
        /// Initiates the scheduler.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public static async void Initiate(IServiceProvider serviceProvider)
        {
            var scheduler = await StartScheduler(serviceProvider);
            await ScheduleJobs(scheduler);
        }

        private static async Task<IScheduler> StartScheduler(IServiceProvider serviceProvider)
        {
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = serviceProvider.GetService(typeof(IJobFactory)) as IJobFactory;
            await scheduler.Start();

            return scheduler;
        }

        private static async Task ScheduleJobs(IScheduler scheduler)
        {
            var bannedUsersDeletionJobDetail = JobBuilder
                .Create<WeeklySummaryEmailJob>()
                .Build();

            var bannedUsersDeletionJobTrigger = TriggerBuilder
                .Create()
                .WithIdentity("Banned users deletion job")
                .StartAt(DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow))
                .WithSimpleSchedule(e =>
                {
                    e.WithIntervalInMinutes(1);
                    e.RepeatForever();
                })
                .Build();

            await scheduler.ScheduleJob(bannedUsersDeletionJobDetail, bannedUsersDeletionJobTrigger);
        }
    }
}
