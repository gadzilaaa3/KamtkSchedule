using KamtkSchedule.Domain.Entities.Base;
using KamtkSchedule.Domain.Entities.Owned;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KamtkSchedule.Domain.Entities
{
    [Table("GroupSchedules", Schema = "dbo")]
    [Index(nameof(GroupId), Name = "IX_GroupSchedules_GroupId")]
    [Index(nameof(WeeklyScheduleId), Name = "IX_GroupSchedules_WeeklyScheduleId")]
    public class GroupSchedule : BaseEntity
    {
        public int? GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty(nameof(Group.GroupSchedules))]
        [JsonPropertyName("group")]
        public Group? GroupNavigation { get; set; }

        public int? WeeklyScheduleId { get; set; }

        [ForeignKey(nameof(WeeklyScheduleId))]
        [InverseProperty(nameof(WeeklySchedule.GroupSchedules))]
        [JsonPropertyName("weeklySchedule")]
        public WeeklySchedule? WeeklyScheduleNavigation { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(ScheduleDay.GroupScheduleNavigation))]
        public ICollection<ScheduleDay> ScheduleDays { get; set; } 
            = new List<ScheduleDay>();
        
        public ScheduleDateInfo DateInfo { get; set; } = new();
    }
}
