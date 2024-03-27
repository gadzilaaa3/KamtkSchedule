using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos.Api;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.Infrastructure.Data;
using KamtkSchedule.Infrastructure.Repositories.Database.Base;
using KamtkSchedule.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KamtkSchedule.Infrastructure.Repositories.Database
{
    public class GroupRepository : BaseRepository<Group, GroupDto>,
        IGroupRepository
    {
        public GroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        internal GroupRepository(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public override IQueryable<GroupDto> SelectDto()
        {
            return Table.Select(e => new GroupDto
            {
                Id = e.Id,
                Name = e.Name,
            });
        }

        public virtual IEnumerable<DisciplineDto> GetGroupDisciplines(int id)
        {
            return Table
                .Where(e => e.Id == id)
                .SelectMany(e => e.Disciplines)
                .Select(e => new DisciplineDto
                { 
                    Id = e.Id,
                    Name = e.Name,
                })
                .ToList();
        }
    }
}
