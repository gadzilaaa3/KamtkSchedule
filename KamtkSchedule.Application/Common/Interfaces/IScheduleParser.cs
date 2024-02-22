using KamtkSchedule.Domain.Entities;

namespace KamtkSchedule.Application.Common.Interfaces
{
    public interface IScheduleParser
    {
        public ScheduleStaffWeek GetScheduleStaffWeekFor(string searchName);

        public ScheduleWeek GetScheduleWeekForAll();
    }
}
