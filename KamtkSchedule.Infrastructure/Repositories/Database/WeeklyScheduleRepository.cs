using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Dtos.Api.ForStudents;
using KamtkSchedule.Domain.Dtos.Api.ForTeachers;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.Infrastructure.Data;
using KamtkSchedule.Infrastructure.Repositories.Database.Base;
using Microsoft.EntityFrameworkCore;

namespace KamtkSchedule.Infrastructure.Repositories.Database
{
    public class WeeklyScheduleRepository : BaseRepository<WeeklySchedule, 
        WeeklyScheduleDto>, 
        IWeeklyScheduleRepository
    {
        public WeeklyScheduleRepository(ApplicationDbContext context)
            : base(context) { }
        internal WeeklyScheduleRepository(
            DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public GroupScheduleDto GetCurrentGroupScheduleForGroup(
            int id)
        {
            var group = Context.Groups?.Find(id)!;
            var now = DateTime.Now.Date;

            var query = Context?.WeeklySchedules?
            .Where(e =>
                now <= e.DateInfo.ScheduleEndDay &&
                now >= e.DateInfo.ScheduleStartDay)
            .SelectMany(e => e.GroupSchedules)
            .Where(e => e.GroupId == group.Id);

            var groupSchedule = query.Select(e => new GroupScheduleDto
            {
                Id = e.Id,
                Group = new GroupDto
                {
                    Id = (int)e.GroupId,
                    Name = e.GroupNavigation.Name,
                },
                ScheduleStartDay = e.DateInfo.ScheduleStartDay,
                ScheduleEndDay = e.DateInfo.ScheduleEndDay,
                ScheduleDays = e.ScheduleDays.Select(e => new ScheduleDayDto 
                {
                    Id = e.Id,
                    Date = e.Date,
                    DayOfWeek = e.DayOfWeek,
                    Pairs = e.Pairs.Select(e => new PairDto
                    {
                        Id = e.Id,
                        Cabinets = e.Cabinets.Select(e => new CabinetDto
                        {
                            Id = e.Id,
                            Name = e.Name,
                        }),
                        Discipline = new DisciplineDto
                        {
                            Id = (int)e.DisciplineId,
                            Name = e.DisciplineNavigation.Name,
                        },
                        PairNumber = e.PairNumber,
                        Teachers = e.Teachers.Select(e => new TeacherDto
                        {
                            Id = e.Id,
                            Name = e.Name,
                        }),
                    }).OrderBy(p => p.PairNumber)
                }).OrderBy(d => d.Date),
            }).OrderBy(e => e.Id).LastOrDefault();

            return groupSchedule!;
        }

        public override IQueryable<WeeklyScheduleDto> SelectDto()
        {
            throw new NotImplementedException();
        }
        public TeacherScheduleDto GetCurrentTeacherScheduleForTeacher(int id)
        {
            var now = DateTime.Now.Date;

            var dateInfo = Context?.WeeklySchedules?
            .Select(w => w.DateInfo)
            .AsNoTracking()
            .FirstOrDefault(e =>
                now <= e.ScheduleEndDay &&
                now >= e.ScheduleStartDay);

            var teacher = Context.Teachers?.Find(id);

            var scheduleDays = new List<ScheduleDayDto>();
            DateTime index = dateInfo.ScheduleStartDay;
            int dayOfWeek = ((int)dateInfo.ScheduleStartDay.DayOfWeek);
            while (index <= dateInfo.ScheduleEndDay)
            {
                scheduleDays.Add(new ScheduleDayDto
                {
                    Date = index,
                    DayOfWeek = (DayOfWeek)dayOfWeek,
                });

                index = index.AddDays(1);
                if (dayOfWeek == 6)
                {
                    dayOfWeek = 0;
                }
                else
                {
                    dayOfWeek++;
                }
            }

            var teacherScheduleDays = Context?.GroupSchedules.Where(e =>
                    now <= e.DateInfo.ScheduleEndDay &&
                    now >= e.DateInfo.ScheduleStartDay)
                .SelectMany(gs => gs.ScheduleDays, (gs, sd) => new ScheduleDayDto
                {
                    Date = sd.Date,
                    DayOfWeek = sd.DayOfWeek,
                    Pairs = sd.Pairs
                        .Where(p => p.Teachers.Any(t => t.Id == teacher.Id))
                        .Select(p => new PairDto
                        {
                            Building = p.GroupNavigation.Building,
                            Cabinets = p.Cabinets.Select(c => new CabinetDto
                            {
                                Id = c.Id,
                                Name = c.Name
                            }),
                            Discipline = new DisciplineDto
                            {
                                Id = p.DisciplineId,
                                Name = p.DisciplineNavigation.Name
                            },
                            Id = p.Id,
                            Group = new GroupDto
                            {
                                Id = p.GroupId,
                                Name = p.GroupNavigation.Name
                            },
                            PairNumber = p.PairNumber,
                            Teachers = p.Teachers.Select(t => new TeacherDto
                            {
                                Id = t.Id,
                                Name = t.Name,
                            })
                        })
                        .OrderBy(p => p.PairNumber),
                }).Where(s => s.Pairs.Any()).OrderBy(sd => sd.Date);

            var pairs = teacherScheduleDays
            .SelectMany(sd => sd.Pairs, (sd, p) => new
            {
                sd.Date,
                sd.DayOfWeek,

                p.Building,
                p.Cabinets,
                p.Discipline,
                p.Id,
                p.Group,
                p.PairNumber,
                p.Teachers,
            });

            var result = new TeacherScheduleDto
            {
                Teacher = new TeacherDto
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                },
                ScheduleStartDay = dateInfo.ScheduleStartDay,
                ScheduleEndDay = dateInfo.ScheduleEndDay,
                ScheduleDays = scheduleDays.GroupJoin(pairs,
                    sd => sd.Date,
                    p => p.Date,
                    (sd, p) => new ScheduleDayDto
                    {
                        Date = sd.Date,
                        DayOfWeek = sd.DayOfWeek,
                        Pairs = p.Select(p => new PairDto
                        {
                            Building = p.Building,
                            Cabinets = p.Cabinets,
                            Discipline = p.Discipline,
                            Group = p.Group,
                            PairNumber = p.PairNumber,
                            Teachers = p.Teachers,
                            Id = p.Id,
                        }).OrderBy(p => p.PairNumber),
                    }),
            };

            return result;
        }
    }
}
