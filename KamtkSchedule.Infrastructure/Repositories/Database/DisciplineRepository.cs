using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.Infrastructure.Data;
using KamtkSchedule.Infrastructure.Repositories.Database.Base;
using Microsoft.EntityFrameworkCore;

namespace KamtkSchedule.Infrastructure.Repositories.Database
{
    public class DisciplineRepository : BaseRepository<Discipline, DisciplineDto>,
        IDisciplineRepository
    {
        public DisciplineRepository(ApplicationDbContext context) : base(context)
        {
        }

        internal DisciplineRepository(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public override IQueryable<DisciplineDto> SelectDto()
        {
            return Table.Select(e => new DisciplineDto
            {
                Id = e.Id,
                Name = e.Name,
            });
        }
    }
}
