using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos.Api.ForStudents;
using KamtkSchedule.Domain.Dtos.Api.ForTeachers;
using KamtkSchedule.Infrastructure.Repositories.Database;
using Microsoft.AspNetCore.Mvc;

namespace KamtkSchedule.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeeklySchedulesController : ControllerBase
    {
        protected IWeeklyScheduleRepository MainRepo;
        public WeeklySchedulesController(IWeeklyScheduleRepository repo) 
        {
            MainRepo = repo;
        }

        [HttpGet("current/for-group/{id}")]
        public ActionResult<GroupScheduleDto> 
            GetCurrentWeeklyScheduleForGroup(int id)
        {
            return Ok(MainRepo.GetCurrentGroupScheduleForGroup(id));
        }

        [HttpGet("current/for-teacher/{id}")]
        public ActionResult<TeacherScheduleDto>
            GetCurrentWeeklyScheduleForTeacher(int id)
        {
            return Ok(MainRepo.GetCurrentTeacherScheduleForTeacher(id));
        }
    }
}
