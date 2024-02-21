using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamtkSchedule.Domain.Entities
{
    public class Schedule
    {
        public DateTime Date { get; set; }
        public string For { get; set; }
        public IEnumerable<Pair> Pairs { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
