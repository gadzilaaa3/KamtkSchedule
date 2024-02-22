using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Domain.Entities;

namespace KamtkSchedule.Infrastructure.Pullers
{
    public class SchedulePuller : ISchedulePuller
    {
        private IScheduleParser _parser;
        private readonly IScheduleParserFactory _factory;

        public SchedulePuller(IScheduleParserFactory factory)
        {
            _factory = factory;
            _parser = _factory.CreateDefaultScheduleParser();
        }

        public ScheduleStaffWeek GetScheduleStaffWeekFor(string searchName)
        {
            return _parser.GetScheduleStaffWeekFor(searchName);
        }

        public ScheduleWeek GetScheduleWeekForAll()
        {
            return _parser.GetScheduleWeekForAll();
        }

        public void SetParser(Func<IScheduleParserFactory, IScheduleParser> func)
        {
            _parser = func.Invoke(_factory);
        }
    }
}
