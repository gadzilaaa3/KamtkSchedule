using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Infrastracture.IntegrationTests.Base;
using KamtkSchedule.Infrastructure.Parsers.Factiories;

namespace KamtkSchedule.Infrastracture.IntegrationTests.IntegrationTests
{
    public class UnitTestForParsers : BaseTest
    {
        [Fact]
        void ShouldReturnGroupSchedule()
        {
            IScheduleParserFactory factory 
                = new HtmlScheduleParserFactory(Configuration);
            var parser = factory.CreateParserBuildingA();

            var groupScheduleDto = parser.GetGroupSchedule("Ì-401");
            Assert.NotNull(groupScheduleDto);
        }
    }
}