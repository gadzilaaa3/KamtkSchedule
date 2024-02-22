using HtmlAgilityPack;
using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Application.Exceptions;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.Domain.Enums;

using System.Globalization;
using System.Net;

namespace KamtkSchedule.Infrastructure.Parsers
{
    public abstract class HtmlScheduleParser : IScheduleParser
    {
        protected readonly HtmlNodeCollection MainNodeCollection;
        protected const int DayOfWeekCount = 7;
        protected CollegeBuilding Building;
        protected StaffRole Role;
        protected string ScheduleFor;

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="UriFormatException"></exception>
        /// <exception cref="WebException"></exception>
        /// <exception cref="InvalidHtmlResourceException"></exception>
        internal HtmlScheduleParser(string url, CollegeBuilding building)
        {
            this.Building = building;

            HtmlWeb web = new();
            var document = web.Load(url);

            MainNodeCollection = document.DocumentNode.SelectNodes("//table/tr") ??
                throw new InvalidHtmlResourceException("Provide a link to the " +
                    "college schedule website");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainNodeCollection"></param>
        internal HtmlScheduleParser(HtmlNodeCollection mainNodeCollection, CollegeBuilding building)
        {
            this.Building = building;
            this.MainNodeCollection = mainNodeCollection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        /// <exception cref="ColumnIndexNotFoundException"></exception>
        protected virtual int FindColumnIndexFor(string searchName)
        {
            try
            {
                var tableRow = MainNodeCollection.First();
                var tableColumns = tableRow.SelectNodes("./td");

                var tableColumn = tableColumns.First(td =>
                {
                    string htmlContent = td.SelectSingleNode("./span").InnerText;
                    return IsContainsSearchName(htmlContent, searchName);
                });

                ScheduleFor = RemoveExtraCharsFromSearchName(tableColumn.SelectSingleNode("./span").InnerText);

                return tableColumns.GetNodeIndex(tableColumn);
            }
            catch (Exception)
            {
                throw new ColumnIndexNotFoundException($"Column index for " +
                    $"{searchName} not found");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="searchName"></param>
        /// <returns></returns>
        protected virtual bool IsContainsSearchName(string original, string searchName)
        {
            return original.Replace("&nbsp;", " ")
                .Contains($"Группа {searchName}",
                    StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchName"></param>
        /// <param name="searchIndex"></param>
        /// <returns></returns>
        /// <exception cref="InvalidHtmlResourceException"></exception>
        protected virtual int[] FindAllIndexesBeginDay(string searchName,
            int searchIndex)
        {
            try
            {
                int[] startDayIndexes = new int[DayOfWeekCount];

                var startDayNodes = MainNodeCollection.Where(tr =>
                {
                    var tableColumn = tr.SelectNodes("./td").ElementAt(searchIndex);
                    string htmlContent = tableColumn.SelectSingleNode("./span").InnerText;
                    return IsContainsSearchName(htmlContent, searchName);
                });

                for (int i = 0; i < startDayNodes.Count(); i++)
                {
                    startDayIndexes[i] = MainNodeCollection
                        .GetNodeIndex(startDayNodes.ElementAt(i));
                }

                return startDayIndexes;
            }
            catch (Exception)
            {
                throw new InvalidHtmlResourceException("The indexes of the beginning " +
                    "of the days were not found");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalName"></param>
        /// <returns></returns>
        protected virtual string RemoveExtraCharsFromSearchName(string originalName)
        {
            return originalName.Replace("&nbsp;", " ")
                .Replace("Группа", "")
                .Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="InvalidHtmlResourceException"></exception>
        protected virtual ScheduleDay ParseScheduleDayFromHtml(
            ParseScheduleFromHtmlOptions options)
        {
            List<Pair> pairs = [];

            try
            {
                var row = MainNodeCollection.ElementAt(options.CurrentDayBeginIndex);

                // Получить дату
                var td = row.SelectNodes("./td")[0];
                string innerTextDate = td.SelectSingleNode("./span").InnerText;
                DateTime date = DateTime.ParseExact(innerTextDate, "dd.MM.yyyy",
                    CultureInfo.InvariantCulture);

                // Получить пары
                int pairNumber = 1;
                for (int i = options.CurrentDayBeginIndex + 1; i < options.NextDayBeginIndex; i++)
                {
                    var currentRow = MainNodeCollection.ElementAt(i);
                    var tColumn = currentRow.SelectNodes("./td")[options.SearchIndex];
                    string disciplineText = tColumn.SelectSingleNode("./span")
                        .InnerText.Replace("&nbsp;", " ").Trim();
                    if (string.IsNullOrEmpty(disciplineText))
                    {
                        pairNumber++;
                        i++;
                        continue;
                    }

                    var nextRow = MainNodeCollection.ElementAt(i + 1);
                    var tc = nextRow.SelectNodes("./td")[options.SearchIndex];
                    string teacherText = tc.SelectSingleNode("./span").InnerText
                        .Replace("&nbsp;", " ").Trim();

                    var cabinetColumn = nextRow.SelectNodes("./td")[options.SearchIndex + 1];
                    string cabinetText = cabinetColumn.SelectSingleNode("./span").InnerText
                        .Replace("&nbsp;", " ").Trim();

                    pairs.Add(new Pair
                    {
                        PairNumber = pairNumber,
                        Discipline = disciplineText,
                        WhoHasAPair = teacherText,
                        CabinetName = cabinetText
                    });

                    pairNumber++;
                    i++;
                }

                return new ScheduleDay
                {
                    DayOfWeek = options.DayOfWeek,
                    Pairs = pairs,
                    Date = date
                };
            }
            catch (Exception)
            {
                throw new InvalidHtmlResourceException("The schedule could " +
                    "not be parsed.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidHtmlResourceException"></exception>
        protected virtual IEnumerable<string> GetAllNames()
        {
            try
            {
                var tableRow = MainNodeCollection.First();
                List<string> groups = [];

                var columns = tableRow.SelectNodes("./td");

                int count = 0;
                for (int i = 2; i < columns.Count - 1; i += 3)
                {
                    count++;

                    groups.Add(RemoveExtraCharsFromSearchName(
                        columns[i].SelectSingleNode("./span").InnerText));

                    if (count % 4 == 0)
                    {
                        i++;
                    }
                }

                return groups;
            }
            catch (Exception)
            {
                throw new InvalidHtmlResourceException("Could not find all the names " +
                    "of the target model");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidHtmlResourceException"></exception>
        /// <exception cref="ColumnIndexNotFoundException"></exception>
        public virtual ScheduleStaffWeek GetScheduleStaffWeekFor(string searchName)
        {
            int searchIndex = FindColumnIndexFor(searchName);

            ScheduleStaffWeek scheduleStaffWeek = new()
            {
                Building = Building,
                Role = Role,
                For = ScheduleFor,
            };

            List<ScheduleDay> scheduleDays = [];

            int[] startDayIndexes = FindAllIndexesBeginDay(searchName,
                searchIndex);

            for (int i = 0; i < startDayIndexes.Length; i++)
            {
                int currentIndex = startDayIndexes[i];
                int nextIndex = MainNodeCollection.Count;
                if (i < startDayIndexes.Length - 1)
                {
                    nextIndex = startDayIndexes[i + 1];
                }

                DayOfWeek dayOfWeek = i switch
                {
                    0 => DayOfWeek.Monday,
                    1 => DayOfWeek.Tuesday,
                    2 => DayOfWeek.Wednesday,
                    3 => DayOfWeek.Thursday,
                    4 => DayOfWeek.Friday,
                    5 => DayOfWeek.Saturday,
                    6 => DayOfWeek.Sunday,
                    _ => throw new InvalidHtmlResourceException(),
                };

                scheduleDays.Add(ParseScheduleDayFromHtml(new ParseScheduleFromHtmlOptions
                {
                    DayOfWeek = dayOfWeek,
                    CurrentDayBeginIndex = currentIndex,
                    SearchIndex = searchIndex,
                    NextDayBeginIndex = nextIndex,
                }));
            }

            scheduleStaffWeek.Days = scheduleDays;
            scheduleStaffWeek.ScheduleStartDay = scheduleDays.First().Date;
            scheduleStaffWeek.ScheduleEndDay = scheduleDays.Last().Date;

            return scheduleStaffWeek;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidHtmlResourceException"></exception>
        /// <exception cref="ColumnIndexNotFoundException"></exception>
        public virtual ScheduleWeek GetScheduleWeekForAll()
        {
            ScheduleWeek scheduleWeek = new();

            List<ScheduleStaffWeek> staffSchedules = [];

            IEnumerable<string> groups = GetAllNames();

            foreach (string group in groups)
            {
                var staffSchedule = GetScheduleStaffWeekFor(group);
                staffSchedules.Add(staffSchedule);
            }
            scheduleWeek.StaffSchedules = staffSchedules;

            return scheduleWeek;
        }
    
        protected record ParseScheduleFromHtmlOptions
        {
            public int CurrentDayBeginIndex { get; set; }
            public int NextDayBeginIndex { get; set; }
            public DayOfWeek DayOfWeek { get; set; }
            public int SearchIndex { get; set; }
        }
    }
}
