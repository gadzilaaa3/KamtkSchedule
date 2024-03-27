using KamtkSchedule.Infrastracture.IntegrationTests.Base;
using KamtkSchedule.Infrastructure.Parsers.Factiories;
using KamtkSchedule.Infrastructure.Pullers;
using KamtkSchedule.Infrastructure.Services;

namespace KamtkSchedule.Infrastracture.IntegrationTests.IntegrationTests
{
    public class UnitTestForServices : BaseTest,
        IClassFixture<EnsureDatabaseTestFixture>
    {
        [Fact]
        void ShouldCreateNewWeeklySchedule()
        {
            var puller = new SchedulePuller(
                new HtmlScheduleParserFactory(Configuration));
            using var scheduleKeeper = new ScheduleKeeper(puller, Context);
            scheduleKeeper.UpdateScheduleBuildingA();

            Assert.Single(Context?.WeeklySchedules?.ToList() ?? []);
        }

        [Fact]
        void WeeklyScheduleShouldRemainTheSame()
        {
            var puller = new SchedulePuller(
                new HtmlScheduleParserFactory(Configuration));
            using var scheduleKeeper = new ScheduleKeeper(puller, Context);
            scheduleKeeper.UpdateScheduleBuildingA();

            var weeklySchedules = Context?.WeeklySchedules?.ToList();
            Assert.Single(weeklySchedules ?? []);

            scheduleKeeper.UpdateScheduleBuildingA();

            Assert.Single(Context?.WeeklySchedules.ToList());
            Assert.Equal(Context?.WeeklySchedules?.ToList(), weeklySchedules);
        }

        [Fact]
        void ShouldCreateNewWeeklySchedulesForTwoBuildings()
        {
            var puller = new SchedulePuller(
                new HtmlScheduleParserFactory(Configuration));
            using var scheduleKeeper = new ScheduleKeeper(puller, Context);
            
            scheduleKeeper.UpdateScheduleBuildingA();
            scheduleKeeper.UpdateScheduleBuildingB();

            int groupCountBuildingA = 32;
            int groupCountBuildingB = 17;
            Assert.Equal(2, Context.WeeklySchedules.Count());
            Assert.Equal(groupCountBuildingA + groupCountBuildingB, 
                Context.Groups.Count());
            Assert.Equal(groupCountBuildingA + groupCountBuildingB, 
                Context.GroupSchedules.Count());
            Assert.Equal(groupCountBuildingA * 7 + groupCountBuildingB * 7, 
                Context.ScheduleDays.Count());
        }
    }
}
