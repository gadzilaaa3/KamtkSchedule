using KamtkSchedule.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KamtkSchedule.Domain.Entities
{
    [Table("Cabinets", Schema = "dbo")]
    public class Cabinet : BaseEntity
    {
        [Required, StringLength(20)]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Pair> Pairs = new List<Pair>();
    }
}
