using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Dtos.Api.ForTeachers;
using Microsoft.AspNetCore.Mvc;

namespace KamtkSchedule.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        protected IScheduleDayRepository MainRepo;

        public SchedulesController(IScheduleDayRepository repo)
        {
            MainRepo = repo;
        }

        [HttpGet("schedule-days/for-group/{id}")]
        public ActionResult<IEnumerable<ScheduleDayDto>> GetScheduleDaysForGroup(int id, 
            [FromQuery] ScheduleDateInfoDto dateInfo)
        {
            return Ok(MainRepo.GetScheduleDaysForGroup(id, dateInfo));
        }

        [HttpGet("schedule-days/for-teacher/{id}")]
        public ActionResult<TeacherScheduleDto> GetTeacherScheduleForSpecifiedDays(int id,
            [FromQuery] ScheduleDateInfoDto dateInfo)
        {
            return Ok(MainRepo.GetTeacherScheduleForSpecifiedDays(id, dateInfo));
        }
    }
}
