using MedCore.Application.Dtos.Medical.AvailibilityModesDto;
using MedCore.Application.Interfaces.Medical;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.medical
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityModesController : ControllerBase
    {
        private readonly IAvailabilityModesService _availabilityModesService;
        public readonly ILogger<AvailabilityModesController> _logger;
        public readonly IConfiguration _configuration;
        public AvailabilityModesController(IAvailabilityModesService availabilityModesService, ILogger<AvailabilityModesController> logger, IConfiguration configuration)
        {
            _availabilityModesService = availabilityModesService;
            _logger = logger;
            _configuration = configuration;
        }

        // GET: api/<AvailabilityModesController>
        [HttpGet("GetAvailability")]
        public async Task<IActionResult> Get()
        {
            var availabilityModes = await _availabilityModesService.GetAll();
            return Ok(availabilityModes);
        }

        // GET api/<AvailabilityModesController>/5
        [HttpGet("GetAvailabilityById")]
        public async Task<IActionResult> Get(short id)
        {
            var availabilityModes = await _availabilityModesService.GetById(id); 
            return Ok(availabilityModes);
        }

        // POST api/<AvailabilityModesController>
        [HttpPost("SaveAvailability")]
        public async Task<IActionResult> Post([FromBody] SaveAvailabilityModesDto availability)

        {
            var availabilityModes = await _availabilityModesService.Save(availability);
            return Ok(availabilityModes);
        }

        // PUT api/<AvailabilityModesController>/5
        [HttpPut("UpdateAvailability")]
        public async Task<IActionResult> Put(short id, [FromBody] UpdateAvailabilityModesDto availabilityModesDto)
        {
            var availabilityModes = await _availabilityModesService.Update(availabilityModesDto);
            return Ok(availabilityModes);
        }

        // DELETE api/<AvailabilityModesController>/5
        [HttpDelete("DeleteAvailability")]

        public async Task<IActionResult> Delete(RemoveAvailabilityModesDto availabilityModesDto)
        {
            var availavilityModes = await _availabilityModesService.Remove(availabilityModesDto);
            return Ok(availavilityModes);
        }

        
        // GET api/<AvailabilityModesController>/5
        [HttpGet("GetAvailabilityModeByNameAsync")]
        public async Task<IActionResult> GetAvailabilityModeByNameAsync(string name)
        {
            var availabilityModes = await _availabilityModesService.GetAvailabilityModeByNameAsync(name);
            return Ok(availabilityModes);
        }
        
    }
}





