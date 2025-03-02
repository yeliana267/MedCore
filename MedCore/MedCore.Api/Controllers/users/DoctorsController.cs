using MedCore.Domain.Entities.users;
using MedCore.Persistence.Interfaces.users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
            private readonly IDoctorsRepository _doctorsRepository;
            public readonly ILogger<DoctorsController> _logger;
            public readonly IConfiguration _configuration;
            public DoctorsController(IDoctorsRepository doctorsRepository, ILogger<DoctorsController> logger, IConfiguration configuration)
            {
                _configuration = configuration;
                _doctorsRepository = doctorsRepository;
                _logger = logger;
            }

        // GET: api/<DoctorsController>
        [HttpGet("GetDoctors")]
        public async Task<IActionResult> Get()
        {
            var patients = await _doctorsRepository.GetAllAsync();
            return Ok(patients);
        }

        // GET api/<DoctorsController>/5
        [HttpGet("GetDoctorByID")]
        public async Task<IActionResult> Get(int id)
        {
            var patient = await _doctorsRepository.GetEntityByIdAsync(id);
            return Ok(patient);
        }

        // POST api/<DoctorsController>
        [HttpPost("Save")]
        public async Task<IActionResult> Post([FromBody] Doctors doctors)
        {
            var patient = await _doctorsRepository.SaveEntityAsync(doctors);
            return Ok(patient);
        }


        // PUT api/<DoctorsController>/5
        [HttpPut("Update")]
        public async Task<IActionResult> Put(int id, [FromBody] Doctors doctors)
        {
            var patient = await _doctorsRepository.UpdateDoctorAsync(id, doctors);
            return Ok(patient);
        }

        // DELETE api/<DoctorsController>/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _doctorsRepository.DeleteDoctorByIdAsync(id);
            return Ok(patient);
        }
    }
}
