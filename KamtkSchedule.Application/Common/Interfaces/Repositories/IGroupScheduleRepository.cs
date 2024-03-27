using KamtkSchedule.Application.Common.Interfaces.Repositories.Base;
using KamtkSchedule.Domain.Dtos.Api.ForStudents;
using KamtkSchedule.Domain.Entities;

namespace KamtkSchedule.Application.Common.Interfaces.Repositories
{
    public interface IGroupScheduleRepository : IRepository<GroupSchedule, GroupScheduleDto>
    {
    }
}
