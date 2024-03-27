using KamtkSchedule.Domain.Enums;

using System.Collections.Generic;

namespace KamtkSchedule.Domain.Dtos.Parsers
{
    public class WeeklyScheduleDto
    {
        public CollegeBuilding Building { get; set; }
        public IEnumerable<GroupScheduleDto> GroupSchedules { get; set; } = [];
        public ScheduleDateInfoDto DateInfo { get; set; } = new();
    }
}
