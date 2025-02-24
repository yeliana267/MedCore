using MedCore.Persistence.Interfaces.medical;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.medical
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityModesController : ControllerBase
    {
        private readonly IAvailabilityModesRepository _availabilityModesRepository;
        private readonly ILogger<AvailabilityModesController> _logger;
        private readonly IConfiguration _configuration;
        public AvailabilityModesController(IAvailabilityModesRepository availabilityModesRepository, ILogger<AvailabilityModesController> logger, IConfiguration configuration)
        {
            _availabilityModesRepository = availabilityModesRepository;
            _logger = logger;
            _configuration = configuration;
        }

        // GET: api/<AvailabilityModesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var availabilityModes = await _availabilityModesRepository.GetAllAsync();
            return Ok();
        }

        // GET api/<AvailabilityModesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AvailabilityModesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AvailabilityModesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AvailabilityModesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
