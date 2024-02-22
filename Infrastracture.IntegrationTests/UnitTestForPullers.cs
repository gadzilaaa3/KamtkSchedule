using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Infrastructure.Factiories;
using KamtkSchedule.Infrastructure.Pullers;
using Microsoft.Extensions.Configuration;

namespace Infrastracture.IntegrationTests
{
    public class UnitTestForPullers
    {
        private readonly IConfiguration _configuration;

        public UnitTestForPullers()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [Fact]
        public void TestSetParserMethodOfSchedulePuller()
        {
            IScheduleParserFactory factory = new HtmlScheduleParserFactory(_configuration);
            var puller = new SchedulePuller(factory);

            puller.SetParser(factory =>
            {
                return factory.CreateTeacherScheduleParserBuildingA();
            });

            var schedule = puller.GetScheduleStaffWeekFor("Белякова");

            Assert.Equal(7, schedule.Days.Count());

            puller.SetParser(factory =>
            {
                return factory.CreateStudentScheduleParserBuildingA();
            });

            schedule = puller.GetScheduleStaffWeekFor("ИП-409");

            Assert.Equal(3, 
                schedule.Days.First(d => d.DayOfWeek == DayOfWeek.Thursday).Pairs.Count());
        }
    }
}
