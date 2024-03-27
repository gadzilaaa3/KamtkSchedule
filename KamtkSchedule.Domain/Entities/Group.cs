using KamtkSchedule.Domain.Entities.Base;
using KamtkSchedule.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KamtkSchedule.Domain.Entities
{
    [Table("Groups", Schema = "dbo")]
    public class Group : BaseEntity
    {
        [Required, StringLength(20)]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        [InverseProperty(nameof(Pair.GroupNavigation))]
        public ICollection<Pair> Pairs { get; set; } = new List<Pair>();

        [JsonIgnore]
        [InverseProperty(nameof(GroupSchedule.GroupNavigation))]
        public ICollection<GroupSchedule> GroupSchedules { get; set; } 
            = new List<GroupSchedule>();

        [JsonIgnore]
        public ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();

        [Required]
        public CollegeBuilding Building { get; set; }
    }
}
