namespace KamtkSchedule.Application.Common.Interfaces
{
    public interface IScheduleParserFactory
    {
        public IScheduleParser CreateDefaultParser();
        public IScheduleParser CreateParserBuildingA();
        public IScheduleParser CreateParserBuildingB();
    }
}
