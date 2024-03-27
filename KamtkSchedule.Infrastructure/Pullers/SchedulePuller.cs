using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Domain.Dtos.Parsers;

namespace KamtkSchedule.Infrastructure.Pullers
{
    public class SchedulePuller : ISchedulePuller
    {
        private IScheduleParser _parser;
        private IScheduleParserFactory _factory;

        public SchedulePuller(IScheduleParserFactory factory)
        {
            _factory = factory;
            _parser = _factory.CreateDefaultParser();
        }

        public GroupScheduleDto GetGroupSchedule(string searchName)
        {
            return _parser.GetGroupSchedule(searchName);
        }

        public WeeklyScheduleDto GetWeeklySchedule()
        {
            return _parser.GetWeeklySchedule();
        }

        public void SetParser(Func<IScheduleParserFactory, IScheduleParser> func)
        {
            _parser = func.Invoke(_factory);
        }

        public void SetParser(IScheduleParser parser)
        {
            _parser = parser;
        }

        public void SetFactory(IScheduleParserFactory factory)
        {
            _factory = factory;
        }
    }
}
