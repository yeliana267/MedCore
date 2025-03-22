using MedCore.Application.Dtos.appointments.DoctorAvailability;
using MedCore.Application.Interfaces.appointments;
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
        private readonly IDoctorAvailabilityService _doctorAvailabilityService;
        public readonly ILogger<DoctorAvailabilityController> _logger;
        public readonly IConfiguration _configuration;
        public DoctorAvailabilityController(IDoctorAvailabilityService doctorAvailabilityService
 
            , ILogger<DoctorAvailabilityController> logger, IConfiguration configuration)
        {
          _doctorAvailabilityService = doctorAvailabilityService;
            _logger = logger;
            _configuration = configuration;
        }




        // GET: api/<DoctorAvailabilityController>
        [HttpGet("GetDoctorAvailability")]
        public async Task<IActionResult> Get()
        {
            var doctorAvailabilities = await _doctorAvailabilityService.GetAll();
            return Ok(doctorAvailabilities);
        }

        // GET api/<DoctorAvailabilityController>/5
        [HttpGet("Doctor")]
        public async Task<IActionResult> Get(int id)
        {
            var doctorAvailabilities = await _doctorAvailabilityService.GetById(id);
            return Ok(doctorAvailabilities);

        }

        // POST api/<DoctorAvailabilityController>
        [HttpPost("SaveDoctorAvailability")]
        public async Task<IActionResult> Post([FromBody] SaveDoctorAvailabilityDto dto)
        {
            var result = await _doctorAvailabilityService.Save(dto);
            return Ok(result);
        }


        // PUT api/<DoctorAvailabilityController>/5
        [HttpPut("UpdateDoctorAvailability")]
        public async Task<IActionResult> Put([FromBody] UpdateDoctorAvailabilityDto dto)
        {
            var result = await _doctorAvailabilityService.Update(dto);
            return Ok(result);
        }


        // DELETE api/<DoctorAvailabilityController>/5
        [HttpDelete("DeleteDoctorAvailability")]
        public async Task<IActionResult> Delete(RemoveDoctorAvailabilityDto dto)
        {
            var result = await _doctorAvailabilityService.Remove(dto);
            return Ok(result);
        }
    }
}
