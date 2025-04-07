using MedCore.Application.Dtos.users.Doctors;
using MedCore.Application.Dtos.users.Users;
using MedCore.Application.Interfaces.users;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Interfaces.users;
using Microsoft.AspNetCore.Mvc;


namespace MedCore.Api.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
            private readonly IDoctorsService _doctorsService;
            public readonly ILogger<DoctorsController> _logger;
            public readonly IConfiguration _configuration;
            public DoctorsController(IDoctorsService doctorsService, ILogger<DoctorsController> logger, IConfiguration configuration)
            {
                _configuration = configuration;
                _doctorsService = doctorsService;
                _logger = logger;
            }

        // GET: api/<DoctorsController>
        [HttpGet("GetDoctors")]
        public async Task<IActionResult> Get()
        {
            var doctors = await _doctorsService.GetAll();
            return Ok(doctors);
        }

        // GET api/<DoctorsController>/5
        [HttpGet("GetDoctorByID")]
        public async Task<IActionResult> Get(int id)
        {
            var doctors = await _doctorsService.GetById(id);
            return Ok(doctors);
        }

        // POST api/<DoctorsController>
        [HttpPost("SaveDoctor")]
        public async Task<IActionResult> Post([FromBody] SaveDoctorsDto doctors)
        {
            var doctor = await _doctorsService.Save(doctors);
            return Ok(doctor);
        }


        // PUT api/<DoctorsController>/5
        [HttpPut("UpdateDoctor")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateDoctorsDto doctors)
        {
            var doctor = await _doctorsService.Update(doctors);
            return Ok(doctor);
        }

        // DELETE api/<DoctorsController>/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] RemoveDoctorsDto id)
        {
            var doctor = await _doctorsService.Remove(id);
            return Ok(doctor);
        }
    }
}
