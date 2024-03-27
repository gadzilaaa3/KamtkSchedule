using KamtkSchedule.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KamtkSchedule.Domain.Entities
{
    [Table("Teachers", Schema = "dbo")]
    public class Teacher : BaseEntity
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Pair> Pairs { get; set; } = new List<Pair>();

        [JsonIgnore]
        public ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();
    }
}
