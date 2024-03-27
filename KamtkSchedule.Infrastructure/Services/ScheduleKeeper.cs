using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Domain.Dtos.Parsers;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.Domain.Enums;
using KamtkSchedule.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KamtkSchedule.Infrastructure.Services
{
    public class ScheduleKeeper : IDisposable
    {
        protected readonly ISchedulePuller Puller;
        protected readonly ApplicationDbContext Context;
        public ScheduleKeeper(ISchedulePuller puller, ApplicationDbContext context)
        {
            Puller = puller;
            Context = context;
        }
        public void UpdateScheduleBuildingA()
        {
            Puller.SetParser(factory
                => factory.CreateParserBuildingA());

            UpdateSchedule();
        }
        public void UpdateScheduleBuildingB()
        {
            Puller.SetParser(factory
                => factory.CreateParserBuildingB());

            UpdateSchedule();
        }

        private void UpdateSchedule()
        {
            WeeklyScheduleDto weeklyScheduleDto = Puller.GetWeeklySchedule();

            WeeklySchedule? weeklyScheduleFound = Context?.WeeklySchedules
                ?.FirstOrDefault(ws =>
                ws.DateInfo.ScheduleStartDay ==
                    weeklyScheduleDto.DateInfo.ScheduleStartDay &&
                ws.DateInfo.ScheduleEndDay ==
                    weeklyScheduleDto.DateInfo.ScheduleEndDay &&
                ws.Building == weeklyScheduleDto.Building);

            if (weeklyScheduleFound == null)
            {
                CreateNewWeeklySchedule(weeklyScheduleDto);
            }
            else
            {
                UpdateExistWeeklySchedule(weeklyScheduleDto, weeklyScheduleFound);
            }
        }
        private void UpdateExistWeeklySchedule(WeeklyScheduleDto weeklyScheduleDto,
            WeeklySchedule weeklyScheduleFound)
        {
            foreach (GroupScheduleDto groupScheduleDto 
                in weeklyScheduleDto.GroupSchedules)
            {
                GroupSchedule? groupScheduleFound =
                    weeklyScheduleFound.GroupSchedules.FirstOrDefault(
                        gs => gs?.GroupNavigation?.Name == groupScheduleDto.Group);

                if (groupScheduleFound == null)
                {
                    GroupSchedule groupSchedule 
                        = CreateNewGroupSchedule(groupScheduleDto, weeklyScheduleDto);
                    weeklyScheduleFound.GroupSchedules.Add(groupSchedule);
                }
                else
                {
                    UpdateExistGroupSchedule(groupScheduleDto, groupScheduleFound);
                }
            }
        }
        private void UpdateExistGroupSchedule(GroupScheduleDto groupScheduleDto,
            GroupSchedule groupScheduleFound)
        {
            foreach (ScheduleDayDto scheduleDayDto 
                in groupScheduleDto.ScheduleDays)
            {
                ScheduleDay? scheduleDayFound = 
                    groupScheduleFound.ScheduleDays.FirstOrDefault(
                        sd => sd.Date == scheduleDayDto.Date);

                if(scheduleDayFound == null)
                {
                    var scheduleDay = CreateNewScheduleDay(scheduleDayDto,
                        groupScheduleFound.GroupNavigation);
                    groupScheduleFound.ScheduleDays.Add(scheduleDay);
                }
                else
                {
                    UpdateExistScheduleDay(scheduleDayDto, scheduleDayFound);
                }
            }
        }

        private void UpdateExistScheduleDay(ScheduleDayDto scheduleDayDto,
            ScheduleDay scheduleDayFound)
        {
            foreach (PairDto pairDto in scheduleDayDto.Pairs)
            {
                Pair? pairFound = scheduleDayFound.Pairs.FirstOrDefault(
                    p => p.PairNumber == pairDto.PairNumber);

                if(pairFound == null)
                {
                    var pair = CreateNewPair(pairDto,
                        scheduleDayFound.GroupScheduleNavigation.GroupNavigation);
                    scheduleDayFound.Pairs.Add(pair);
                }
                else
                {
                    UpdateExistPair(pairDto, pairFound);
                }
            }
            // Удалить пары если их убрали...
            foreach (Pair pair in scheduleDayFound.Pairs)
            {
                if(scheduleDayDto.Pairs.FirstOrDefault(
                    sd => sd.PairNumber == pair.PairNumber) == null)
                {
                    scheduleDayFound.Pairs.Remove(pair);
                }
            }
        }

        private void UpdateExistPair(PairDto pairDto, Pair pairFound)
        {
            var discipline = EnsureExistDiscipline(pairDto);
            if(pairFound.DisciplineNavigation.Name != pairDto.Discipline)
            {
                pairFound.DisciplineNavigation = discipline;
            }

            var teachers = EnsureExistTeachers(pairDto, discipline);
            foreach (var teacher in teachers)
            {
                if (pairFound.Teachers.FirstOrDefault(
                    t => t.Id == teacher.Id) == null)
                {
                    pairFound.Teachers = teachers;
                    break;
                }
            }

            var cabinets = EnsureExistCabinets(pairDto);
            foreach (var cabinet in cabinets)
            {
                if(pairFound.Cabinets.FirstOrDefault(
                    c => c.Id == cabinet.Id) == null)
                {
                    pairFound.Cabinets = cabinets;
                    break;
                }
            }
        }

        private void CreateNewWeeklySchedule(WeeklyScheduleDto weeklyScheduleDto)
        {
            WeeklySchedule weeklySchedule = new()
            {
                Building = weeklyScheduleDto.Building,
                DateInfo = new()
                {
                    ScheduleStartDay =
                        weeklyScheduleDto.DateInfo.ScheduleStartDay,
                    ScheduleEndDay =
                        weeklyScheduleDto.DateInfo.ScheduleEndDay,
                },
                GroupSchedules = CreateNewGroupSchedules(weeklyScheduleDto)
            };

            Context?.WeeklySchedules?.Add(weeklySchedule);
            Context?.SaveChanges();
        }

        private List<GroupSchedule> CreateNewGroupSchedules(
            WeeklyScheduleDto weeklyScheduleDto)
        {
            List<GroupSchedule> groupSchedules = [];
            foreach (GroupScheduleDto groupScheduleDto
                in weeklyScheduleDto.GroupSchedules)
            {
                groupSchedules.Add(CreateNewGroupSchedule(groupScheduleDto, 
                    weeklyScheduleDto));
            }

            return groupSchedules;
        }

        private GroupSchedule CreateNewGroupSchedule(GroupScheduleDto groupScheduleDto,
            WeeklyScheduleDto weeklyScheduleDto)
        {
            Group groupFound = EnsureExistGroup(groupScheduleDto, weeklyScheduleDto.Building);

            return new GroupSchedule()
            {
                GroupNavigation = groupFound,
                DateInfo = new()
                {
                    ScheduleStartDay =
                    weeklyScheduleDto.DateInfo.ScheduleStartDay,
                    ScheduleEndDay =
                    weeklyScheduleDto.DateInfo.ScheduleEndDay,
                },
                ScheduleDays = CreateNewScheduleDays(groupScheduleDto.ScheduleDays,
                    groupFound)
            };
        }

        private Group EnsureExistGroup(GroupScheduleDto groupScheduleDto, 
            CollegeBuilding building)
        {
            Group? groupFound = Context?.Groups?.Include(g => g.Disciplines)
                .FirstOrDefault(g => g.Name == groupScheduleDto.Group
                    && g.Building == building);

            if (groupFound == null)
            {
                groupFound = new()
                {
                    Name = groupScheduleDto.Group,
                };
                Context?.Groups?.Add(groupFound);
                Context?.SaveChanges();
            }

            return groupFound;
        }

        private List<ScheduleDay> CreateNewScheduleDays(
            IEnumerable<ScheduleDayDto> scheduleDaysDto, Group groupFound)
        {
            List<ScheduleDay> scheduleDays = [];

            foreach (ScheduleDayDto scheduleDayDto
                in scheduleDaysDto)
            {
                scheduleDays.Add(CreateNewScheduleDay(scheduleDayDto, groupFound));
            }

            return scheduleDays;
        }

        private ScheduleDay CreateNewScheduleDay(ScheduleDayDto scheduleDayDto,
            Group groupFound)
        {
            return new ScheduleDay()
            {
                Date = scheduleDayDto.Date,
                DayOfWeek = scheduleDayDto.DayOfWeek,
                Pairs = CreateNewPairs(scheduleDayDto.Pairs, groupFound),
            };
        }

        private List<Pair> CreateNewPairs(IEnumerable<PairDto> pairsDto,
            Group groupFound)
        {
            List<Pair> pairs = [];
            foreach (PairDto pairDto in pairsDto)
            {
                pairs.Add(CreateNewPair(pairDto, groupFound));
            }

            return pairs;
        }

        private Pair CreateNewPair(PairDto pairDto, Group groupFound)
        {
            List<Cabinet> cabinets = EnsureExistCabinets(pairDto);

            Discipline disciplineFound = EnsureExistDiscipline(pairDto);
            EnsureGroupContainsDiscipline(groupFound, disciplineFound);
            List<Teacher> teachers = EnsureExistTeachers(pairDto, disciplineFound);

            return new Pair()
            {
                PairNumber = pairDto.PairNumber,
                DisciplineNavigation = disciplineFound,
                Cabinets = cabinets,
                Teachers = teachers,
                GroupNavigation = groupFound,
            };
        }

        private List<Teacher> EnsureExistTeachers(PairDto pairDto, 
            Discipline disciplineFound)
        {
            List<Teacher> teachers = [];
            foreach (string teacher in pairDto.Teachers)
            {
                Teacher teacherFound = EnsureExistTeacher(teacher);

                EnsureTeacherContainsDiscipline(disciplineFound, teacherFound);

                teachers.Add(teacherFound);
            }

            return teachers;
        }

        private Teacher EnsureExistTeacher(string teacher)
        {
            Teacher? teacherFound = Context?.Teachers
                ?.Include(t => t.Disciplines)
                ?.FirstOrDefault(t => t.Name == teacher);

            if (teacherFound == null)
            {
                teacherFound = new()
                {
                    Name = teacher,
                };
                Context?.Teachers?.Add(teacherFound);
                Context?.SaveChanges();
            }

            return teacherFound;
        }

        private void EnsureTeacherContainsDiscipline(Discipline disciplineFound, 
            Teacher teacherFound)
        {
            if (teacherFound.Disciplines.FirstOrDefault(
                                d => d.Id == disciplineFound.Id) == null)
            {
                teacherFound.Disciplines.Add(disciplineFound);
                Context?.SaveChanges();
            }
        }

        private void EnsureGroupContainsDiscipline(Group groupFound, 
            Discipline disciplineFound)
        {
            if (groupFound.Disciplines.FirstOrDefault(
                d => d.Id == disciplineFound.Id) == null)
            {
                groupFound.Disciplines.Add(disciplineFound);
                Context?.SaveChanges();
            }
        }

        private List<Cabinet> EnsureExistCabinets(PairDto pairDto)
        {
            List<Cabinet> cabinets = [];
            foreach (string cabinet in pairDto.Cabinets)
            {
                Cabinet cabinetFound = EnsureExistCabinet(cabinet);
                cabinets.Add(cabinetFound);
            }

            return cabinets;
        }

        private Discipline EnsureExistDiscipline(PairDto pairDto)
        {
            Discipline? disciplineFound = Context?.Disciplines
                            ?.FirstOrDefault(d => d.Name == pairDto.Discipline);
            if (disciplineFound == null)
            {
                disciplineFound = new()
                {
                    Name = pairDto.Discipline,
                };
                Context?.Disciplines?.Add(disciplineFound);
                Context?.SaveChanges();
            }

            return disciplineFound;
        }

        private Cabinet EnsureExistCabinet(string cabinet)
        {
            Cabinet? cabinetFound = Context?.Cabinets
                ?.FirstOrDefault(c => c.Name == cabinet);

            if (cabinetFound == null)
            {
                cabinetFound = new()
                {
                    Name = cabinet,
                };
                Context?.Cabinets?.Add(cabinetFound);
                Context?.SaveChanges();
            }

            return cabinetFound;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool _isDisposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            if (disposing)
            {
                Context.Dispose();
            }
            _isDisposed = true;
        }
        ~ScheduleKeeper()
        {
            Dispose(false);
        }
    }
}
