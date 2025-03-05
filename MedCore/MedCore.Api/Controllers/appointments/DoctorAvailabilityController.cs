using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.appointments
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAvailabilityController : ControllerBase
    {
        public readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        public readonly ILogger<DoctorAvailabilityController> _logger;
        public readonly IConfiguration _configuration;
        public DoctorAvailabilityController( IDoctorAvailabilityRepository doctorAvailabilityRepository 
            , ILogger<DoctorAvailabilityController> logger, IConfiguration configuration)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _logger = logger;
            _configuration = configuration;
        }




        // GET: api/<DoctorAvailabilityController>
        [HttpGet("GetDoctorAvailability")]
        public async Task<IActionResult> Get()
        {
            var result = await _doctorAvailabilityRepository.GetAllAsync();
            return Ok(result);
        }

        // GET api/<DoctorAvailabilityController>/5
        [HttpGet("Doctor")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _doctorAvailabilityRepository.GetEntityByIdAsync(id);
            return Ok(result);

        }

        // POST api/<DoctorAvailabilityController>
        [HttpPost("SaveDoctorAvailability")]
        public async Task<IActionResult> Post([FromBody] DoctorAvailability doctorAvailability)
        {
            var result = await _doctorAvailabilityRepository.SaveEntityAsync(doctorAvailability);
            return Ok(result);
        }

        // PUT api/<DoctorAvailabilityController>/5
        [HttpPut("UpdateDoctorAvailability")]
        public async Task<IActionResult> Put(int id, [FromBody] DoctorAvailability doctorAvailability)
        {
            var result = await _doctorAvailabilityRepository.UpdateEntityAsync(id, doctorAvailability);
            return Ok(result);
        }

        // DELETE api/<DoctorAvailabilityController>/5
        [HttpDelete("DeleteDoctorAvailability")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorAvailabilityRepository.DeleteEntityByIdAsync(id);
            return Ok(result);
        }
    }
}
