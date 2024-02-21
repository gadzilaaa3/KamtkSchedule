using HtmlAgilityPack;
using KamtkSchedule.Application.Exceptions;
using System.Net;

namespace KamtkSchedule.Infrastructure.Parsers
{
    public class HtmlStudentScheduleParser : HtmlScheduleParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <exception cref="UriFormatException"></exception>
        /// <exception cref="WebException"></exception>
        /// <exception cref="InvalidHtmlResourceException"></exception>
        public HtmlStudentScheduleParser(string url) : base(url) { }

        public HtmlStudentScheduleParser(HtmlNodeCollection mainNodeCollection) 
            : base(mainNodeCollection) { }
    }
}
