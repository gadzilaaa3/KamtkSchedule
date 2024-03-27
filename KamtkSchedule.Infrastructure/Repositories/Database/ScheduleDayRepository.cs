using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Application.Exceptions.Api;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Dtos.Api.ForTeachers;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.Infrastructure.Data;
using KamtkSchedule.Infrastructure.Repositories.Database.Base;
using Microsoft.EntityFrameworkCore;

namespace KamtkSchedule.Infrastructure.Repositories.Database
{
    public class ScheduleDayRepository : BaseRepository<ScheduleDay, ScheduleDayDto>,
        IScheduleDayRepository
    {
        private const int scheduleDaysLimit = 14;

        public ScheduleDayRepository(ApplicationDbContext context) : base(context)
        {
        }

        internal ScheduleDayRepository(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateInfo"></param>
        /// <exception cref="IncorrectlyProvidedDataException"></exception>
        private static void IsValidScheduleDateInfoDto(ScheduleDateInfoDto dateInfo)
        {
            var diff = dateInfo.ScheduleEndDay - dateInfo.ScheduleStartDay;
            if (diff.Days > scheduleDaysLimit || diff.Days < 0)
            {
                throw new IncorrectlyProvidedDataException($"Specify a shorter interval " +
                    $"in the schedule so that it is less than {scheduleDaysLimit} days");
            }
        }

        public IEnumerable<ScheduleDayDto> GetScheduleDaysForGroup(int id, 
            ScheduleDateInfoDto dateInfo)
        {
            IsValidScheduleDateInfoDto(dateInfo);

            var scheduleDays = Context.ScheduleDays
                .Where(sd =>
                    sd.Date >= dateInfo.ScheduleStartDay &&
                    sd.Date <= dateInfo.ScheduleEndDay &&
                    sd.GroupScheduleNavigation.GroupId == id)
                .Select(sd => new ScheduleDayDto
                {
                    Id = sd.Id,
                    Date = sd.Date,
                    DayOfWeek = sd.DayOfWeek,
                    Pairs = sd.Pairs.Select(p => new PairDto
                    {
                        Id = p.Id,
                        Cabinets = p.Cabinets.Select(c => new CabinetDto
                        {
                            Id = c.Id,
                            Name = c.Name,
                        }),
                        Discipline = new DisciplineDto
                        {
                            Id = p.DisciplineId,
                            Name = p.DisciplineNavigation.Name
                        },
                        PairNumber = p.PairNumber,
                        Teachers = p.Teachers.Select(t => new TeacherDto
                        {
                            Id = t.Id,
                            Name = t.Name,
                        }),
                        Group = new GroupDto
                        {
                            Id = p.GroupId,
                            Name = p.GroupNavigation.Name,
                        }
                    })
                })
                .OrderBy(sd => sd.Date);

            return scheduleDays;
        }

        public TeacherScheduleDto GetTeacherScheduleForSpecifiedDays(int id,
            ScheduleDateInfoDto dateInfo)
        {
            IsValidScheduleDateInfoDto(dateInfo);

            var teacher = Context.Teachers.Find(id);

            var scheduleDays = new List<ScheduleDayDto>();
            DateTime index = dateInfo.ScheduleStartDay;
            while (index <= dateInfo.ScheduleEndDay)
            {
                scheduleDays.Add(new ScheduleDayDto
                {
                    Date = index,
                    DayOfWeek = index.DayOfWeek,
                });

                index = index.AddDays(1);
            }

            var pairs = Context.ScheduleDays
                .Where(sd =>
                    sd.Date >= dateInfo.ScheduleStartDay &&
                    sd.Date <= dateInfo.ScheduleEndDay)
                .SelectMany(sd => sd.Pairs, (sd, p) => new
                {
                    sd.Date,
                    p.GroupNavigation.Building,
                    p.Cabinets,
                    Discipline = p.DisciplineNavigation,
                    p.Id,
                    Group = p.GroupNavigation,
                    p.PairNumber,
                    p.Teachers,
                })
                .Where(p => p.Teachers.Any(t => t.Id == teacher.Id));

            return new TeacherScheduleDto
            {
                ScheduleStartDay = dateInfo.ScheduleStartDay,
                ScheduleEndDay = dateInfo.ScheduleEndDay,
                Teacher = new TeacherDto
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                },
                ScheduleDays = scheduleDays.GroupJoin(pairs,
                    sd => sd.Date,
                    p => p.Date,
                    (sd, p) => new ScheduleDayDto
                    {
                        Date = sd.Date,
                        DayOfWeek = sd.DayOfWeek,
                        Pairs = p.Select(p => new PairDto
                        {
                            Id = p.Id,
                            Building = p.Building,
                            Cabinets = p.Cabinets.Select(c => new CabinetDto
                            {
                                Id = c.Id,
                                Name = c.Name,
                            }),
                            Discipline = new DisciplineDto
                            {
                                Id = p.Discipline.Id,
                                Name = p.Discipline.Name,
                            },
                            Group = new GroupDto 
                            { 
                                Id = p.Group.Id,
                                Name = p.Group.Name,
                            },
                            PairNumber = p.PairNumber,
                            Teachers = p.Teachers.Select(t => new TeacherDto
                            {
                                Id = t.Id,
                                Name = t.Name,
                            })
                        }).OrderBy(p => p.PairNumber),
                    }).OrderBy(sd => sd.Date),
            };
        }
        
        public override IQueryable<ScheduleDayDto> SelectDto()
        {
            throw new NotImplementedException();
        }
    }
}
