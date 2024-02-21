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
        public void Test1()
        {
            ScheduleParserFactory factory = new HtmlScheduleParserFactory(_configuration);
            var puller = new SchedulePuller(factory);

            puller.SetParser(factory =>
            {
                return factory.CreateTeacherScheduleParserBuildingA();
            });

            var schedules = puller.GetSchedulesFor("Белякова");

            Assert.Equal(7, schedules.Count());

            puller.SetParser(factory =>
            {
                return factory.CreateStudentScheduleParserBuildingA();
            });

            schedules = puller.GetSchedulesFor("ИП-409");

            Assert.Equal(4, 
                schedules.First(s => s.DayOfWeek == DayOfWeek.Wednesday)
                .Pairs.Count());
        }
    }
}
