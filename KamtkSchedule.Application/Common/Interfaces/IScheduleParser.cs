using KamtkSchedule.Domain.Dtos.Parsers;

namespace KamtkSchedule.Application.Common.Interfaces
{
    public interface IScheduleParser
    {
        public GroupScheduleDto GetGroupSchedule(string searchName);

        public WeeklyScheduleDto GetWeeklySchedule();
    }
}
