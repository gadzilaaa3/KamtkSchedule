using KamtkSchedule.Domain.Dtos.Api.Base;
using KamtkSchedule.Domain.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KamtkSchedule.Domain.Dtos.Api.Common
{
    public class PairDto : BaseEntityDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GroupDto? Group { get; set; }
        public IEnumerable<TeacherDto> Teachers
        { get; set; } = [];
        public DisciplineDto Discipline { get; set; } = new();
        public IEnumerable<CabinetDto> Cabinets
        { get; set; } = [];
        public int PairNumber { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CollegeBuilding? Building { get; set; }
    }
}
