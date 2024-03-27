using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace KamtkSchedule.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class GroupsController : BaseReadController<Group,
        GroupsController, GroupDto>
    {
        public GroupsController(IGroupRepository repo) : base(repo)
        {
        }

        [HttpGet("{id}/disciplines")]
        public ActionResult<IEnumerable<DisciplineDto>> GetGroupDisciplines(
            int id)
        {
            return Ok(((IGroupRepository)MainRepo).GetGroupDisciplines(id));
        }
    }
}
