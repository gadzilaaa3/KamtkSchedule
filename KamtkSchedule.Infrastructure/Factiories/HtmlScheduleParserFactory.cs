using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Domain.Enums;
using KamtkSchedule.Infrastructure.Parsers;
using Microsoft.Extensions.Configuration;

namespace KamtkSchedule.Infrastructure.Factiories
{
    public class HtmlScheduleParserFactory : IScheduleParserFactory
    {
        private readonly IConfiguration _configuration;
        public HtmlScheduleParserFactory(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public IScheduleParser CreateDefaultScheduleParser()
        {
            return CreateStudentScheduleParserBuildingA();
        }

        public IScheduleParser CreateStudentScheduleParserBuildingA()
        {
            string url = _configuration
                .GetRequiredSection("ScheduleLinks")
                .GetRequiredSection("Students")["BuildingA"] ?? "";

            return new HtmlStudentScheduleParser(url, CollegeBuilding.A);
        }

        public IScheduleParser CreateStudentScheduleParserBuildingB()
        {
            string url = _configuration
                .GetRequiredSection("ScheduleLinks")
                .GetRequiredSection("Students")["BuildingB"] ?? "";

            return new HtmlStudentScheduleParser(url, CollegeBuilding.B);
        }

        public IScheduleParser CreateTeacherScheduleParserBuildingA()
        {
            string url = _configuration
                .GetRequiredSection("ScheduleLinks")
                .GetRequiredSection("Teachers")["BuildingA"] ?? "";

            return new HtmlTeacherScheduleParser(url, CollegeBuilding.A);
        }

        public IScheduleParser CreateTeacherScheduleParserBuildingB()
        {
            string url = _configuration
                .GetRequiredSection("ScheduleLinks")
                .GetRequiredSection("Teachers")["BuildingB"] ?? "";

            return new HtmlTeacherScheduleParser(url, CollegeBuilding.B);
        }
    }
}
