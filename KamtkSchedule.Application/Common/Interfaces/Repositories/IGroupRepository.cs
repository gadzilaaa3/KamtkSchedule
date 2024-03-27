using KamtkSchedule.Application.Common.Interfaces.Repositories.Base;
using KamtkSchedule.Domain.Dtos.Api;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Entities;
using System.Collections.Generic;

namespace KamtkSchedule.Application.Common.Interfaces.Repositories
{
    public interface IGroupRepository : IRepository<Group, GroupDto>
    {
        IEnumerable<DisciplineDto> GetGroupDisciplines(int id);
    }
}
