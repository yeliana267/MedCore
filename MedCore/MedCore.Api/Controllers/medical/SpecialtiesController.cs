using MedCore.Application.Dtos.Medical.SpecialtiesDto;
using MedCore.Application.Interfaces.Medical;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.medical
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtiesService _specialtiesServices;
        private readonly ILogger<SpecialtiesController> _logger;
        private readonly IConfiguration _configuration;
        public SpecialtiesController(ISpecialtiesService specialtiesService, ILogger<SpecialtiesController> logger, IConfiguration configuration)
        {
            _specialtiesServices = specialtiesService;
            _logger = logger;
            _configuration = configuration;
        }

        // GET: api/<SpecialtiesController>
        [HttpGet("GetSpecialties")]
        public async Task<IActionResult> Get()
        {
            var Specialties = await _specialtiesServices.GetAll();
            return Ok(Specialties);
        }

        // GET api/<SpecialtiesController>/5
        [HttpGet("GetSpecialtyById")]
        public async Task<IActionResult> Get(short id)
        {
            var Specialties = await _specialtiesServices.GetById(id); //raro
            return Ok(Specialties);
        }

        // POST api/<SpecialtiesController>
        [HttpPost("SaveSpecialties")]
        public async Task<IActionResult> Post([FromBody] SaveSpecialtiesDto specialtiesDto)
        {
            var result = await _specialtiesServices.Save(specialtiesDto);
            return Ok(result);
        }

        // PUT api/<SpecialtiesController>/5
        [HttpPut("UpdateSpecialties")]
        public async Task<IActionResult> Put(short id, [FromBody] UpdateSpecialtiesDto specialtiesDto)
        {
            specialtiesDto.SpecialtiesId = id;
            var result = await _specialtiesServices.Update(specialtiesDto);
            return NoContent();
        }

        // DELETE api/<SpecialtiesController>/5
        [HttpDelete("DeleteSpecialties")]
        public async Task<IActionResult> Delete(RemoveSpecialtiesDto specialtiesDto)
        {
            var Specialties = await _specialtiesServices.Remove(specialtiesDto);
            return NoContent();
        }

       
        [HttpGet("GetSpecialtyByNameAsync")]
        public async Task<IActionResult> GetSpecialtyByNameAsync(string name)
        {
            var specialties = await _specialtiesServices.GetSpecialtyByNameAsync(name);
            return Ok(specialties);
        }
        
    }
}
