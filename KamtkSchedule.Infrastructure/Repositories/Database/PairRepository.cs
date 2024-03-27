using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.Infrastructure.Data;
using KamtkSchedule.Infrastructure.Repositories.Database.Base;
using Microsoft.EntityFrameworkCore;

namespace KamtkSchedule.Infrastructure.Repositories.Database
{
    public class PairRepository : BaseRepository<Pair, PairDto>, IPairRepository
    {
        public PairRepository(ApplicationDbContext context) : base(context)
        {
        }

        internal PairRepository(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public override IQueryable<PairDto> SelectDto()
        {
            throw new NotImplementedException();
        }
    }
}
