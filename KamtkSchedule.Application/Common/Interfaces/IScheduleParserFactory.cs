namespace KamtkSchedule.Application.Common.Interfaces
{
    public interface IScheduleParserFactory
    {
        public IScheduleParser CreateStudentScheduleParserBuildingA();
        public IScheduleParser CreateStudentScheduleParserBuildingB();
        public IScheduleParser CreateTeacherScheduleParserBuildingA();
        public IScheduleParser CreateTeacherScheduleParserBuildingB();
        public IScheduleParser CreateDefaultScheduleParser();
    }
}
