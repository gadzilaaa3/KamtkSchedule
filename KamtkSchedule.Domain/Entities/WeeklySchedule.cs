using KamtkSchedule.Domain.Entities.Base;
using KamtkSchedule.Domain.Entities.Owned;
using KamtkSchedule.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KamtkSchedule.Domain.Entities
{
    public class WeeklySchedule : BaseEntity
    {
        [Required]
        public CollegeBuilding Building { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(GroupSchedule.WeeklyScheduleNavigation))]
        public ICollection<GroupSchedule> GroupSchedules { get; set; } 
            = new List<GroupSchedule>();

        public ScheduleDateInfo DateInfo { get; set; } = new();
    }
}
