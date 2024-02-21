using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Infrastructure.Parsers;
using Microsoft.Extensions.Configuration;

namespace KamtkSchedule.Infrastructure.Factiories
{
    public class HtmlScheduleParserFactory : ScheduleParserFactory
    {
        private readonly IConfiguration _configuration;
        public HtmlScheduleParserFactory(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public override IScheduleParser CreateDefaultScheduleParser()
        {
            return CreateStudentScheduleParserBuildingA();
        }

        public override IScheduleParser CreateStudentScheduleParserBuildingA()
        {
            string url = _configuration
                .GetRequiredSection("ScheduleLinks")
                .GetRequiredSection("Students")["BuildingA"] ?? "";

            return new HtmlStudentScheduleParser(url);
        }

        public override IScheduleParser CreateStudentScheduleParserBuildingB()
        {
            string url = _configuration
                .GetRequiredSection("ScheduleLinks")
                .GetRequiredSection("Students")["BuildingB"] ?? "";

            return new HtmlStudentScheduleParser(url);
        }

        public override IScheduleParser CreateTeacherScheduleParserBuildingA()
        {
            string url = _configuration
                .GetRequiredSection("ScheduleLinks")
                .GetRequiredSection("Teachers")["BuildingA"] ?? "";

            return new HtmlTeacherScheduleParser(url);
        }

        public override IScheduleParser CreateTeacherScheduleParserBuildingB()
        {
            string url = _configuration
                .GetRequiredSection("ScheduleLinks")
                .GetRequiredSection("Teachers")["BuildingB"] ?? "";

            return new HtmlTeacherScheduleParser(url);
        }
    }
}
