using KamtkSchedule.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KamtkSchedule.Domain.Entities
{
    [Table("Pairs", Schema = "dbo")]
    [Index(nameof(GroupId), Name = "IX_Pairs_GroupId")]
    [Index(nameof(ScheduleDayId), Name = "IX_Pairs_ScheduleDayId")]
    [Index(nameof(DisciplineId), Name = "IX_Pairs_DisciplineId")]
    public class Pair : BaseEntity
    {
        public int? GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty(nameof(Group.Pairs))]
        [JsonPropertyName("group")]
        public Group? GroupNavigation { get; set; }

        public int? ScheduleDayId { get; set; }

        [ForeignKey(nameof(ScheduleDayId))]
        [InverseProperty(nameof(ScheduleDay.Pairs))]
        [JsonPropertyName("scheduleDay")]

        public ScheduleDay? ScheduleDayNavigation { get; set; }

        public int? DisciplineId { get; set; }

        [ForeignKey(nameof(DisciplineId))]
        [InverseProperty(nameof(Discipline.Pairs))]
        [JsonPropertyName("discipline")]
        public Discipline? DisciplineNavigation { get; set; }

        [Required]
        public int PairNumber { get; set; }

        [JsonIgnore]
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

        [JsonIgnore]
        public ICollection<Cabinet> Cabinets { get; set; } = new List<Cabinet>();
    }
}
