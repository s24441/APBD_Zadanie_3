using DataAccess.Db.DTOs;
using DataAccess.Enums;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly ILogger<AnimalsController> _logger;
        private readonly IPjatkRepository _repository;

        public AnimalsController(IPjatkRepository repository, ILogger<AnimalsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<AnimalDto>> GetAnimals(AnimalProperty orderProp)
        {
            var data = await _repository.GetAnimals(orderProp);
            return data;
        }

        [HttpPost]
        public async Task<OkResult> AddAnimal([FromBody] AnimalDto dto)
        {
            await _repository.AddAnimal(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<OkResult> AddAnimal(int id, [FromBody] AnimalDto dto)
        {
            await _repository.AddAnimal(dto);
            return Ok();
        }
    }
}
