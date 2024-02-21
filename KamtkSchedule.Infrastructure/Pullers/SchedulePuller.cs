using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.Infrastructure.Factiories;

namespace KamtkSchedule.Infrastructure.Pullers
{
    public class SchedulePuller : ISchedulePuller
    {
        private IScheduleParser _parser;
        private readonly ScheduleParserFactory _factory;

        public SchedulePuller(ScheduleParserFactory factory)
        {
            _factory = factory;
            _parser = _factory.CreateDefaultScheduleParser();
        }

        public IEnumerable<Schedule> GetSchedulesFor(string searchName)
        {
            return _parser.GetSchedulesFor(searchName);
        }

        public IEnumerable<Schedule> GetSchedulesForAll()
        {
            return _parser.GetSchedulesForAll();
        }

        public void SetParser(Func<ScheduleParserFactory, IScheduleParser> func)
        {
            _parser = func.Invoke(_factory);
        }
    }
}
