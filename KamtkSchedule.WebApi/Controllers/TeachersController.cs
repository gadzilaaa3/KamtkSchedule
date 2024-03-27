using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace KamtkSchedule.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TeachersController : BaseReadController<Teacher,
        TeachersController, TeacherDto>
    {
        public TeachersController(ITeacherRepository repo) : base(repo)
        {
        }

        [HttpGet("{id}/disciplines")]
        public ActionResult<IEnumerable<DisciplineDto>> GetTeacherDisciplines(
            int id)
        {
            return Ok(((ITeacherRepository)MainRepo).GetTeacherDisciplines(id));
        }
    }
}
