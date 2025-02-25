using MedCore.Domain.Entities.medical;
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
        [HttpGet("GetAvailability")]
        public async Task<IActionResult> Get()
        {
            var availabilityModes = await _availabilityModesRepository.GetAllAsync();
            return Ok(availabilityModes);
        }

        // POST api/<AvailabilityModesController>
        [HttpPost("SaveAvailability")]
        public async Task<IActionResult> Post([FromBody] AvailabilityModes availability)

        {
            var availabilityModes = await _availabilityModesRepository.SaveEntityAsync(availability);
            return Ok(availabilityModes);
        }

        // PUT api/<AvailabilityModesController>/5
        [HttpPut("UpdateAvailability")]
        public async Task<IActionResult> Put(short id, [FromBody] AvailabilityModes availability)
        {
            var availabilityModes = await _availabilityModesRepository.UpdateEntityAsync(availability);
            return Ok(availabilityModes);
        }
        
        // DELETE api/<AvailabilityModesController>/5
        [HttpDelete("DeleteAvailability")]

        public async Task<IActionResult> Delete(short id)
        {
            var availabilityMode = await _availabilityModesRepository.DeleteAvailabilityModeAsync(id);
            return Ok();
        }
        
        // GET api/<AvailabilityModesController>/5
        [HttpGet("GetRecentlyUpdatedModesAsync")]
        public async Task<IActionResult> Get(int days)
        {
            var availabilityModes = await _availabilityModesRepository.GetRecentlyUpdatedModesAsync(days);
            return Ok(availabilityModes);
        }

        // GET api/<AvailabilityModesController>/5
        [HttpGet("GetAvailabilityModeByNameAsync")]
        public async Task<IActionResult> Get(string name)
        {
            var availabilityMode = await _availabilityModesRepository.GetAvailabilityModeByNameAsync(name);
            return Ok(availabilityMode);
        }
    }
}





