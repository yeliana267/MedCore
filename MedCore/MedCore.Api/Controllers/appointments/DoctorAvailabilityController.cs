using MedCore.Persistence.Interfaces.appointments;
using MedCore.Persistence.Repositories.appointments;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.appointments
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAvailabilityController : ControllerBase
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        public readonly ILogger<DoctorAvailabilityController> _logger;
        public readonly IConfiguration _configuration;
        public DoctorAvailabilityController(IDoctorAvailabilityRepository doctorAvailabilityRepository, ILogger<DoctorAvailabilityController> logger, IConfiguration configuration)
        {

            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _logger = logger;
            _configuration = configuration;
        }




        // GET: api/<DoctorAvailabilityController>
        [HttpGet("GetdoctorAvailabities")]
        public async Task<IActionResult> Get()
        {
            var doctorAvailabilities = await _doctorAvailabilityRepository.GetAllAsync();
            return Ok(doctorAvailabilities);
        }

        // GET api/<DoctorAvailabilityController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DoctorAvailabilityController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DoctorAvailabilityController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoctorAvailabilityController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
