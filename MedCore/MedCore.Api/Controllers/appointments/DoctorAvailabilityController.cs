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
        public readonly ILogger<DoctorAvailabilityController> _logger;
        public readonly IConfiguration _configuration;
        public readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        public DoctorAvailabilityController(ILogger<DoctorAvailabilityController> logger, IConfiguration configuration, IDoctorAvailabilityRepository doctorAvailabilityRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
        }


        // GET: api/<DoctorAvailabilityController>
        [HttpGet("GetDoctorDisponible")]
        public async Task<IActionResult> Get()
        {
           var availability = await _doctorAvailabilityRepository.GetAllAsync();
            return Ok(availability);
        }

        // GET api/<DoctorAvailabilityController>/5
        [HttpGet("obtenerID")]
        public async Task<IActionResult> Get(int id)
        {
            var availability = await _doctorAvailabilityRepository.GetEntityByIdAsync(id);
            return Ok(availability);
        }

        // POSTapi/<DoctorAvailabilityController>
        [HttpPost("SaveDoctorAvalibi")]
        public async Task<IActionResult> Post([FromBody] DoctorAvailability doctorAvailability)
        {
            var availability = await _doctorAvailabilityRepository.SaveEntityAsync(doctorAvailability);
            return Ok(availability);
        }


        // PUT api/<DoctorAvailabilityController>/5
        [HttpPut("UpdateAppointments")]
        public async Task<IActionResult> Put(int id, [FromBody] DoctorAvailability doctorAvailability)
        {
            var availability = await _doctorAvailabilityRepository.UpdateEntityAsync(id, doctorAvailability);
            return Ok(availability);
        }

        // DELETE api/<DoctorAvaililityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var availability = await _doctorAvailabilityRepository.DeleteEntityByIdAsync(id);
            return Ok(availability);
        }
    }
}
