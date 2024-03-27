using System;
using System.Collections.Generic;

namespace KamtkSchedule.Domain.Dtos.Parsers
{
    public class ScheduleDayDto
    {
        public DateTime Date { get; set; }
        public IEnumerable<PairDto> Pairs { get; set; } = [];
        public DayOfWeek DayOfWeek { get; set; }
    }
}
