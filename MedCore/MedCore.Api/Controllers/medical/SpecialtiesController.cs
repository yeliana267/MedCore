using MedCore.Domain.Entities;
using MedCore.Domain.Entities.medical;
using MedCore.Persistence.Interfaces.medical;
using MedCore.Persistence.Repositories.medical;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.medical
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtiesRepository _specialtiesRepository;
        private readonly ILogger<SpecialtiesController> _logger;
        private readonly IConfiguration _configuration;
        public SpecialtiesController(ISpecialtiesRepository specialtiesRepository, ILogger<SpecialtiesController> logger, IConfiguration configuration)
        {
            _specialtiesRepository = specialtiesRepository;
            _logger = logger;
            _configuration = configuration;
        }

        // GET: api/<SpecialtiesController>
        [HttpGet("GetSpecialties")]
        public async Task<IActionResult> Get()
        {
            var Specialties = await _specialtiesRepository.GetAllAsync();
            return Ok(Specialties);
        }

        // POST api/<SpecialtiesController>
        [HttpPost("SaveSpecialties")]
        public async Task<IActionResult> Post([FromBody] Specialties specialties)
        {
            var Specialties = await _specialtiesRepository.SaveEntityAsync(specialties);
            return Ok(specialties);
        }

        // PUT api/<SpecialtiesController>/5
        [HttpPut("UpdateSpecialties")]
        public async Task<IActionResult> Put(short id, [FromBody] Specialties specialties)
        {
            var Specialties = await _specialtiesRepository.UpdateEntityAsync(id, specialties);
            return NoContent();
        }

        // DELETE api/<SpecialtiesController>/5
        [HttpDelete("DeleteSpecialties")]
        public async Task<IActionResult> Delete(short id)
        {
            var Specialties = await _specialtiesRepository.DeleteSpecialtyAsync(id);
            return NoContent();
        }

        // GET api/<SpecialtiesController>/5
        [HttpGet("GetSpecialtyByNameAsync")]
        public async Task<IActionResult> GetSpecialtyByNameAsync(string name)
        {
            var Specialties = await _specialtiesRepository.GetSpecialtyByNameAsync(name);
            return Ok(Specialties);
        }
    }
}
