using KamtkSchedule.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamtkSchedule.Domain.Entities
{
    public class ScheduleWeek
    {
        public IEnumerable<ScheduleStaffWeek> StaffSchedules { get; set; }
    }
}
