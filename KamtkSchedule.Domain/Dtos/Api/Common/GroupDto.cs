using KamtkSchedule.Domain.Dtos.Api.Base;

namespace KamtkSchedule.Domain.Dtos.Api.Common
{
    public class GroupDto : BaseEntityDto
    {
        public string Name { get; set; } = null!;
    }
}
