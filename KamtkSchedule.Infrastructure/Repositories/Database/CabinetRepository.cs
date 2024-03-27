using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.Infrastructure.Data;
using KamtkSchedule.Infrastructure.Repositories.Database.Base;
using Microsoft.EntityFrameworkCore;

namespace KamtkSchedule.Infrastructure.Repositories.Database
{
    public class CabinetRepository : BaseRepository<Cabinet, CabinetDto>,
        ICabinetRepository
    {
        public CabinetRepository(ApplicationDbContext context) 
            : base(context)
        {
        }

        internal CabinetRepository(
            DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public override IQueryable<CabinetDto> SelectDto()
        {
            return Table.Select(e => new CabinetDto
            {
                Id = e.Id,
                Name = e.Name,
            });
        }
    }
}
