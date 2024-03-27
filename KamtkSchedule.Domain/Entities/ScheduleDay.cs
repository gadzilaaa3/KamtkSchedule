using KamtkSchedule.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KamtkSchedule.Domain.Entities
{
    [Table("ScheduleDays", Schema = "dbo")]
    [Index(nameof(GroupScheduleId), Name = "IX_ScheduleDays_GroupScheduleId")]
    public class ScheduleDay : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Pair.ScheduleDayNavigation))]
        public ICollection<Pair> Pairs { get; set; } = new List<Pair>();

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        public int? GroupScheduleId { get; set; }

        [ForeignKey(nameof(GroupScheduleId))]
        [InverseProperty(nameof(GroupSchedule.ScheduleDays))]
        [JsonPropertyName("groupSchedule")]
        public GroupSchedule? GroupScheduleNavigation { get; set; }
    }
}
