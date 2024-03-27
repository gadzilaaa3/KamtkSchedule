using KamtkSchedule.Domain.Dtos.Api.Base;
using KamtkSchedule.Domain.Dtos.Api.Common;
using System;
using System.Collections.Generic;

namespace KamtkSchedule.Domain.Dtos.Api.ForStudents
{
    public class GroupScheduleDto : BaseEntityDto
    {
        public IEnumerable<ScheduleDayDto> ScheduleDays
        { get; set; } = [];
        public DateTime ScheduleStartDay { get; set; }
        public DateTime ScheduleEndDay { get; set; }
        public GroupDto Group { get; set; } = new();
    }
}
