using KamtkSchedule.Application.Common.Interfaces.Repositories.Base;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Entities;
using System.Collections.Generic;

namespace KamtkSchedule.Application.Common.Interfaces.Repositories
{
    public interface ITeacherRepository : IRepository<Teacher, TeacherDto>
    {
        IEnumerable<DisciplineDto> GetTeacherDisciplines(int id);
    }
}
