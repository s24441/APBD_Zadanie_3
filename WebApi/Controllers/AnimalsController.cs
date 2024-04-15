using DataAccess.Db.DTOs;
using DataAccess.Enums;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

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

        [HttpGet("FindIdByName")]
        public async Task<ActionResult<int>> FindIdByName(string name)
        {
            var result = await _repository.GetIdByName(name);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("GetAnimals")]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAnimals(AnimalProperty? orderProp)
        {
            var result = await _repository.GetAnimals(orderProp);
            
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpPost("AddAnimal")]
        public async Task<IActionResult> AddAnimal([FromBody] AnimalDto dto)
        {
            var result = await _repository.AddAnimal(dto);
            return result ? Ok(dto) : BadRequest(dto);
        }

        [HttpPut("UpdateAnimal")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] AnimalDto dto)
        {
            var result = await _repository.UpdateAnimal(id, dto);
            return result switch { 
                true => Ok(),
                false => this.NotModified("Not modified"),
                _ => NotFound()
            };
        }

        [HttpDelete("DeleteAnimal")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var result = await _repository.DeleteAnimal(id);
            return  result ? Ok() : NotFound();
        }
    }
}
