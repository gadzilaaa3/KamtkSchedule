using KamtkSchedule.Infrastructure.Services;
using Quartz;
using Quartz.Impl.Triggers;

namespace KamtkSchedule.WebApi.Jobs
{
    public class UpdateSchedulesJob : IJob
    {
        public static readonly JobKey Key = new(nameof(UpdateSchedulesJob));
        private readonly ScheduleKeeper _sheduleKeeper;
        public UpdateSchedulesJob(ScheduleKeeper scheduleKeeper)
        {
            _sheduleKeeper = scheduleKeeper;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _sheduleKeeper.UpdateScheduleBuildingA();
                _sheduleKeeper.UpdateScheduleBuildingB();

                SimpleTriggerImpl newTrigger = new(Guid.NewGuid().ToString())
                {
                    RepeatCount = 0,
                    JobKey = context.JobDetail.Key,
                    // Вынести в конфиг
                    StartTimeUtc =
                        DateBuilder.TomorrowAt(6, 30, 0),
                };
                await context.Scheduler.ScheduleJob(newTrigger);
            }
            catch (Exception ex)
            {
                SimpleTriggerImpl retryTrigger = new(Guid.NewGuid().ToString())
                {
                    RepeatCount = 0,
                    JobKey = context.JobDetail.Key,
                    StartTimeUtc =
                        DateBuilder.EvenHourDateAfterNow()
                };
                await context.Scheduler.ScheduleJob(retryTrigger);

                JobExecutionException jex = new(ex, false);
                throw jex;
            }
        }
    }
}
