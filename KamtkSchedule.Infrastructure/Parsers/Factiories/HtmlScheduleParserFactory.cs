using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace KamtkSchedule.Infrastructure.Parsers.Factiories
{
    public class HtmlScheduleParserFactory : IScheduleParserFactory
    {
        private readonly IConfiguration _configuration;
        public HtmlScheduleParserFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IScheduleParser CreateDefaultParser()
        {
            return CreateParserBuildingA();
        }

        public IScheduleParser CreateParserBuildingA()
        {
            string url = _configuration
                .GetRequiredSection("ScheduleLinks")
                .GetRequiredSection("Students")["BuildingA"] ?? "";

            return new HtmlStudentScheduleParser(url, CollegeBuilding.A);
        }

        public IScheduleParser CreateParserBuildingB()
        {
            string url = _configuration
                .GetRequiredSection("ScheduleLinks")
                .GetRequiredSection("Students")["BuildingB"] ?? "";

            return new HtmlStudentScheduleParser(url, CollegeBuilding.B);
        }
    }
}
