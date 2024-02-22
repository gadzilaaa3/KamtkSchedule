namespace KamtkSchedule.Application.Common.Interfaces
{
    public interface ISchedulePuller : IScheduleParser
    {
        public void SetParser(Func<IScheduleParserFactory, IScheduleParser> func);
    }
}
