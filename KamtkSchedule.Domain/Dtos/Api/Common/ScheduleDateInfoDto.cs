using System;
using System.ComponentModel.DataAnnotations;

namespace KamtkSchedule.Domain.Dtos.Api.Common
{
    public class ScheduleDateInfoDto
    {
        [Required]
        public DateTime ScheduleStartDay { get; set; }
        [Required]
        public DateTime ScheduleEndDay { get; set; }
    }
}
