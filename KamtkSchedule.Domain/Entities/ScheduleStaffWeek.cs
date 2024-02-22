using KamtkSchedule.Domain.Enums;

namespace KamtkSchedule.Domain.Entities
{
    public class ScheduleStaffWeek
    {
        public CollegeBuilding Building { get; set; }
        public StaffRole Role { get; set; }
        public string For { get; set; }
        public IEnumerable<ScheduleDay> Days { get; set; }
        public DateTime ScheduleStartDay { get; set; }
        public DateTime ScheduleEndDay { get; set; }
    }
}
