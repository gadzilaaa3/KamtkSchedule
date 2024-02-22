using HtmlAgilityPack;
using KamtkSchedule.Application.Exceptions;
using KamtkSchedule.Domain.Enums;

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
        public HtmlStudentScheduleParser(string url, CollegeBuilding building) : base(url, building) {
            Role = StaffRole.Student;
        }

        public HtmlStudentScheduleParser(HtmlNodeCollection mainNodeCollection, 
            CollegeBuilding building) 
            : base(mainNodeCollection, building) 
        {
            Role = StaffRole.Student;
        }
    }
}
