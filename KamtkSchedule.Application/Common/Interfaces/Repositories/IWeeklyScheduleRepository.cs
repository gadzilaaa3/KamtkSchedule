using KamtkSchedule.Application.Common.Interfaces.Repositories.Base;
using KamtkSchedule.Domain.Dtos.Api.ForStudents;
using KamtkSchedule.Domain.Dtos.Api.ForTeachers;
using KamtkSchedule.Domain.Entities;

namespace KamtkSchedule.Application.Common.Interfaces.Repositories
{
    public interface IWeeklyScheduleRepository : IRepository<WeeklySchedule, 
        WeeklyScheduleDto>
    {
        public GroupScheduleDto GetCurrentGroupScheduleForGroup(int id);
        public TeacherScheduleDto GetCurrentTeacherScheduleForTeacher(int id);
    }
}
