using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos.Api.Common;
using KamtkSchedule.Domain.Entities;
using KamtkSchedule.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace KamtkSchedule.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DisciplinesController : BaseReadController<Discipline,
        DisciplinesController, DisciplineDto>
    {
        public DisciplinesController(IDisciplineRepository repo) 
            : base(repo) { }
    }
}
