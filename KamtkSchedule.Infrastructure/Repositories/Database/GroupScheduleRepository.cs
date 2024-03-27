using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos.Api.ForStudents;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.Infrastructure.Data;
using KamtkSchedule.Infrastructure.Repositories.Database.Base;
using Microsoft.EntityFrameworkCore;

namespace KamtkSchedule.Infrastructure.Repositories.Database
{
    public class GroupScheduleRepository : BaseRepository<GroupSchedule, GroupScheduleDto>,
        IGroupScheduleRepository
    {
        public GroupScheduleRepository(ApplicationDbContext context) : base(context)
        {
        }

        internal GroupScheduleRepository(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public override IQueryable<GroupScheduleDto> SelectDto()
        {
            throw new NotImplementedException();
        }
    }
}
