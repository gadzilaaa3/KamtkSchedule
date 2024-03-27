using KamtkSchedule.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KamtkSchedule.Domain.Entities
{
    [Table("Disciplines", Schema = "dbo")]
    public class Discipline : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        [InverseProperty(nameof(Pair.DisciplineNavigation))]
        public ICollection<Pair> Pairs { get; set; } = new List<Pair>();

        [JsonIgnore]
        public ICollection<Group> Groups { get; set; } = new List<Group>();

        [JsonIgnore]
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
