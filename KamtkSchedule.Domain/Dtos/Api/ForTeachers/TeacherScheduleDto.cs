using System.Collections.Generic;
using System;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Dtos.Api.ForStudents;

namespace KamtkSchedule.Domain.Dtos.Api.ForTeachers
{
    public class TeacherScheduleDto
    {
        public IEnumerable<ScheduleDayDto> ScheduleDays
        { get; set; } = [];
        public DateTime ScheduleStartDay { get; set; }
        public DateTime ScheduleEndDay { get; set; }
        public TeacherDto Teacher { get; set; } = new();
    }
}
