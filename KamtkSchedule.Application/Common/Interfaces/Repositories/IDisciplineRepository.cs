using KamtkSchedule.Application.Common.Interfaces.Repositories.Base;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Entities;

namespace KamtkSchedule.Application.Common.Interfaces.Repositories
{
    public interface IDisciplineRepository : IRepository<Discipline, DisciplineDto>
    {
    }
}
