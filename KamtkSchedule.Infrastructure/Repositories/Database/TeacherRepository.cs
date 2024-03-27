using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.Infrastructure.Data;
using KamtkSchedule.Infrastructure.Repositories.Database.Base;
using Microsoft.EntityFrameworkCore;

namespace KamtkSchedule.Infrastructure.Repositories.Database
{
    public class TeacherRepository : BaseRepository<Teacher, TeacherDto>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext context)
            : base(context) { }
        internal TeacherRepository(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public IEnumerable<DisciplineDto> GetTeacherDisciplines(int id)
        {
            return Table
                .Where(e => e.Id == id)
                .SelectMany(e => e.Disciplines)
                .Select(e => new DisciplineDto
                {
                    Id = e.Id,
                    Name = e.Name,
                });
        }

        public override IQueryable<TeacherDto> SelectDto()
        {
            return Table.Select(e => new TeacherDto
            {
                Id = e.Id,
                Name = e.Name,
            });
        }
    }
}
