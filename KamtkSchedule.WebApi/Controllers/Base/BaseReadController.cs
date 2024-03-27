using KamtkSchedule.Application.Common.Interfaces.Repositories.Base;
using KamtkSchedule.Domain.Dtos.Api.Base;
using KamtkSchedule.Domain.Entities.Base;
using KamtkSchedule.Domain.Pagination;
using KamtkSchedule.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;

namespace KamtkSchedule.WebApi.Controllers.Base
{
    [ApiController]
    public abstract class BaseReadController<T, TController, TDto> 
        : ControllerBase
        where T : BaseEntity, new()
        where TController : BaseReadController<T, TController, TDto>
        where TDto : BaseEntityDto
    {
        protected readonly IRepository<T, TDto> MainRepo;

        protected BaseReadController(IRepository<T, TDto> repo) 
        {
            MainRepo = repo;
        }

        [HttpGet("{id}")]
        public async Task<TDto?> GetOneById(int id)
        {
            return await MainRepo.FindOneAsync(id);
        }

        [HttpGet]
        public IActionResult GetManyWithPaginate(
            [FromQuery] PaginatedParameters parameters)
        {
            var items = MainRepo.FindManyWithPaginate(parameters);

            PaginationMetadata metadata = new()
            {
                CurrentPage = items.CurrentPage,
                HasNext = items.HasNext,
                HasPrevious = items.HasPrevious,
                TotalCount = items.TotalCount,
                TotalPages = items.TotalPages
            };

            Response.Headers.Append("X-Pagination", 
                System.Text.Json.JsonSerializer.Serialize(metadata, 
                    WebApiConfiguration.GetJsonSerializerOptions()));

            return Ok(items);
        }
    }
}
