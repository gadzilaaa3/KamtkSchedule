using KamtkSchedule.Domain.Dtos.Api.Base;
using KamtkSchedule.Domain.Enums;
using System;
using System.Collections.Generic;

namespace KamtkSchedule.Domain.Dtos.Api.ForStudents
{
    public class WeeklyScheduleDto : BaseEntityDto
    {
        public CollegeBuilding Building { get; set; }
        public DateTime ScheduleStartDay { get; set; }
        public DateTime ScheduleEndDay { get; set; }
        public IEnumerable<GroupScheduleDto> GroupSchedules
        { get; set; } = [];
    }
}
