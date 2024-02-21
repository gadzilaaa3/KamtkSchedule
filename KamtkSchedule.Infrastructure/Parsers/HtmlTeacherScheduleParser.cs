using HtmlAgilityPack;

namespace KamtkSchedule.Infrastructure.Parsers
{
    public class HtmlTeacherScheduleParser : HtmlScheduleParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <exception cref="UriFormatException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        /// <exception cref="Application.Exceptions.InvalidHtmlResourceException">
        /// </exception>
        public HtmlTeacherScheduleParser(string url) : base(url) { }

        public HtmlTeacherScheduleParser(HtmlNodeCollection mainNodeCollection) 
            : base(mainNodeCollection) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalName"></param>
        /// <returns></returns>
        protected override string RemoveExtraCharsFromSearchName(string originalName) 
            => originalName.Replace("&nbsp;", " ").Trim();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="searchName"></param>
        /// <returns></returns>
        protected override bool IsContainsSearchName(string original, string searchName) 
            => RemoveExtraCharsFromSearchName(original).Contains(searchName);
    }
}
