using System;

namespace KamtkSchedule.Application.Common.Interfaces
{
    public interface ISchedulePuller : IScheduleParser
    {
        public void SetParser(Func<IScheduleParserFactory, IScheduleParser> func);
        public void SetParser(IScheduleParser parser);
        public void SetFactory(IScheduleParserFactory factory);
    }
}
