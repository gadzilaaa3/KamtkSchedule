using KamtkSchedule.Application.Exceptions;
using KamtkSchedule.Infrastructure.Parsers;
using HtmlAgilityPack;

namespace Infrastracture.IntegrationTests
{
    public class UnitTestForParsers
    {
        private static readonly string _urlScheduleForStudentBuildingA 
            = "https://kamtk.ru:9096/el-zurnal/el-dnevnik/obrabotka1.php?student=student%2F000000001";
        private static readonly string _urlScheduleForStudentBuildingB 
            = "https://kamtk.ru:9096/el-zurnal/el-dnevnik/obrabotka1.php?student=student%2F000000002";
        private static readonly string _urlScheduleForTeacherBuildingA
            = "https://kamtk.ru:9096/el-zurnal/el-dnevnik/obrabotka1.php?techer=techer%2F000000001";
        private static readonly string _urlScheduleForTeacherBuildingB
            = "https://kamtk.ru:9096/el-zurnal/el-dnevnik/obrabotka1.php?techer=techer%2F000000002";

        [Fact]
        public void ShouldReturnSchedulesForValidGroupNameBuildingA()
        {
            HtmlScheduleParser parser = new HtmlStudentScheduleParser(_urlScheduleForStudentBuildingA);
            
            var schedules = parser.GetSchedulesFor("ИП-409");
            Assert.Equal(7, schedules.Count());
        }

        [Fact]
        public void ShouldReturnSchedulesForValidGroupNameBuildingB()
        {
            HtmlScheduleParser parser = new HtmlStudentScheduleParser(_urlScheduleForStudentBuildingB);

            var schedules = parser.GetSchedulesFor("ПКД-113");
            Assert.Equal(7, schedules.Count());
        }

        [Fact]
        public void ShouldReturnSchedulesForValidTeacherNameBuildingA()
        {
            HtmlScheduleParser parser = 
                new HtmlTeacherScheduleParser(_urlScheduleForTeacherBuildingA);

            var schedules = parser.GetSchedulesFor("Бадьина");
            Assert.Equal(7, schedules.Count());
        }

        [Fact]
        public void ShouldReturnSchedulesForValidTeacherNameBuildingB()
        {
            HtmlScheduleParser parser = 
                new HtmlTeacherScheduleParser(_urlScheduleForTeacherBuildingB);

            var schedules = parser.GetSchedulesFor("Заставная");
            Assert.Equal(7, schedules.Count());
        }

        [Fact]
        public void ShouldReturnSchedulesForAllGroupBuildingA()
        {
            HtmlScheduleParser parser =
                new HtmlStudentScheduleParser(_urlScheduleForStudentBuildingA);

            var schedules = parser.GetSchedulesForAll();
            Assert.Equal(34, schedules.Count());
        }

        [Fact]
        public void ShouldReturnSchedulesForAllGroupBuildingB()
        {
            HtmlScheduleParser parser =
                new HtmlStudentScheduleParser(_urlScheduleForStudentBuildingB);

            var schedules = parser.GetSchedulesForAll();
            Assert.Equal(17, schedules.Count());
        }

        [Fact]
        public void ShouldThrowInvalidHtmlResourceExceptionForInvalidUrl()
        {
            Assert.Throws<InvalidHtmlResourceException>(() =>
            {
                HtmlScheduleParser parser =
                    new HtmlStudentScheduleParser("https://www.google.ru/");
            });
        }

        [Fact]
        public void ShouldThrowInvalidHtmlResourceExceptionForInvalidHtmlNode()
        {
            HtmlWeb web = new HtmlWeb();
            var _document = web.Load(_urlScheduleForStudentBuildingA);

            Assert.Throws<InvalidHtmlResourceException>(() =>
            {
                var parser = new HtmlStudentScheduleParser(
                    _document.DocumentNode.ChildNodes);

                parser.GetSchedulesForAll();
            });
        }
    }
}