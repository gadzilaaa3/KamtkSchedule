using HtmlAgilityPack;
using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Application.Exceptions;
using KamtkSchedule.Domain.Entities;
using System.Globalization;
using System.Net;

namespace KamtkSchedule.Infrastructure.Parsers
{
    public abstract class HtmlScheduleParser : IScheduleParser
    {
        protected readonly HtmlNodeCollection _mainNodeCollection;
        protected const int DayOfWeekCount = 7;

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="UriFormatException"></exception>
        /// <exception cref="WebException"></exception>
        /// <exception cref="InvalidHtmlResourceException"></exception>
        internal HtmlScheduleParser(string url)
        {
            HtmlWeb web = new();
            var document = web.Load(url);

            _mainNodeCollection = document.DocumentNode.SelectNodes("//table/tr") ??
                throw new InvalidHtmlResourceException("Provide a link to the " +
                    "college schedule website");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainNodeCollection"></param>
        internal HtmlScheduleParser(HtmlNodeCollection mainNodeCollection)
        {
            _mainNodeCollection = mainNodeCollection;
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
                var tableRow = _mainNodeCollection.First();
                var tableColumns = tableRow.SelectNodes("./td");

                var tableColumn = tableColumns.First(td =>
                {
                    string htmlContent = td.SelectSingleNode("./span").InnerText;
                    return IsContainsSearchName(htmlContent, searchName);
                });

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

                var startDayNodes = _mainNodeCollection.Where(tr =>
                {
                    var tableColumn = tr.SelectNodes("./td").ElementAt(searchIndex);
                    string htmlContent = tableColumn.SelectSingleNode("./span").InnerText;
                    return IsContainsSearchName(htmlContent, searchName);
                });

                for (int i = 0; i < startDayNodes.Count(); i++)
                {
                    startDayIndexes[i] = _mainNodeCollection
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
        protected virtual Schedule ParseScheduleFromHtml(
            ParseScheduleFromHtmlOptions options)
        {
            List<Pair> pairs = [];

            try
            {
                // Получить название группы
                var row = _mainNodeCollection.ElementAt(options.CurrentDayBeginIndex);
                var tableColumn = row.SelectNodes("./td")[options.SearchIndex];
                string innerText = tableColumn.SelectSingleNode("./span").InnerText;
                string searchName = RemoveExtraCharsFromSearchName(innerText);

                // Получить дату
                var td = row.SelectNodes("./td")[0];
                string innerTextDate = td.SelectSingleNode("./span").InnerText;
                DateTime date = DateTime.ParseExact(innerTextDate, "dd.MM.yyyy",
                    CultureInfo.InvariantCulture);

                // Получить пары
                int pairNumber = 1;
                for (int i = options.CurrentDayBeginIndex + 1; i < options.NextDayBeginIndex; i++)
                {
                    var currentRow = _mainNodeCollection.ElementAt(i);
                    var tColumn = currentRow.SelectNodes("./td")[options.SearchIndex];
                    string disciplineText = tColumn.SelectSingleNode("./span")
                        .InnerText.Replace("&nbsp;", " ").Trim();
                    if (string.IsNullOrEmpty(disciplineText))
                    {
                        pairNumber++;
                        i++;
                        continue;
                    }

                    var nextRow = _mainNodeCollection.ElementAt(i + 1);
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

                return new Schedule
                {
                    DayOfWeek = options.DayOfWeek,
                    Pairs = pairs,
                    For = searchName,
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
                var tableRow = _mainNodeCollection.First();
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
        public virtual IEnumerable<Schedule> GetSchedulesFor(string searchName)
        {
            int searchIndex = FindColumnIndexFor(searchName);

            List<Schedule> schedules = [];

            int[] startDayIndexes = FindAllIndexesBeginDay(searchName,
                searchIndex);

            for (int i = 0; i < startDayIndexes.Length; i++)
            {
                int currentIndex = startDayIndexes[i];
                int nextIndex = _mainNodeCollection.Count;
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

                schedules.Add(ParseScheduleFromHtml(new ParseScheduleFromHtmlOptions
                {
                    DayOfWeek = dayOfWeek,
                    CurrentDayBeginIndex = currentIndex,
                    SearchIndex = searchIndex,
                    NextDayBeginIndex = nextIndex,
                }));
            }

            return schedules;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidHtmlResourceException"></exception>
        /// <exception cref="ColumnIndexNotFoundException"></exception>
        public virtual IEnumerable<Schedule> GetSchedulesForAll()
        {
            List<Schedule> schedules = [];

            IEnumerable<string> groups = GetAllNames();

            foreach (string group in groups)
            {
                var schedule = GetSchedulesFor(group);
                schedules.AddRange(schedule);
            }

            return schedules;
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
