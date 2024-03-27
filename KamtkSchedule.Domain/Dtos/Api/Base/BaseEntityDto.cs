using System.Text.Json.Serialization;

namespace KamtkSchedule.Domain.Dtos.Api.Base
{
    public abstract class BaseEntityDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Id { get; set; }
    }
}
