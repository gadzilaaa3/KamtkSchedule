using KamtkSchedule.Application.Common.Interfaces.Repositories.Base;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Dtos.Api.ForStudents;
using KamtkSchedule.Domain.Dtos.Api.ForTeachers;
using KamtkSchedule.Domain.Entities;
using System.Collections.Generic;

namespace KamtkSchedule.Application.Common.Interfaces.Repositories
{
    public interface IScheduleDayRepository : IRepository<ScheduleDay, ScheduleDayDto>
    {
        public IEnumerable<ScheduleDayDto> GetScheduleDaysForGroup(int id, 
            ScheduleDateInfoDto dateInfo);

        TeacherScheduleDto GetTeacherScheduleForSpecifiedDays(int id,
            ScheduleDateInfoDto dateInfo);
    }
}
