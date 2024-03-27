using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Infrastructure.Parsers.Factiories;
using KamtkSchedule.Infrastructure.Pullers;
using Microsoft.Extensions.Configuration;

namespace KamtkSchedule.Infrastracture.IntegrationTests.IntegrationTests
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
        void ShouldReturnWeeklyScheduleBuildingA()
        {
            ISchedulePuller puller = new SchedulePuller(
                new HtmlScheduleParserFactory(_configuration));

            puller.SetParser(factory =>
                factory.CreateParserBuildingA());

            var weeklySchedule = puller.GetWeeklySchedule();

            Assert.NotNull(weeklySchedule);
        }
    }
}
