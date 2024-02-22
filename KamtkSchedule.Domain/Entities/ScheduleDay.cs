using System;

namespace KamtkSchedule.Domain.Entities
{
    public class ScheduleDay
    {
        public DateTime Date { get; set; }
        public IEnumerable<Pair> Pairs { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
