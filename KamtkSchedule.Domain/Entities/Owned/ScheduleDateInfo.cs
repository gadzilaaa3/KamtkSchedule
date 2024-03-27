using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace KamtkSchedule.Domain.Entities.Owned
{
    [Owned]
    public class ScheduleDateInfo
    {
        [Required]
        public DateTime ScheduleStartDay { get; set; }
        [Required]
        public DateTime ScheduleEndDay { get; set; }
    }
}
