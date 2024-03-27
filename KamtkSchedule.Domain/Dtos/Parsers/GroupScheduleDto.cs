using System.Collections.Generic;

namespace KamtkSchedule.Domain.Dtos.Parsers
{
    public class GroupScheduleDto
    {
        public string Group { get; set; } = string.Empty;
        public IEnumerable<ScheduleDayDto> ScheduleDays { get; set; } = [];
        public ScheduleDateInfoDto DateInfo { get; set; } = new();
    }
}
