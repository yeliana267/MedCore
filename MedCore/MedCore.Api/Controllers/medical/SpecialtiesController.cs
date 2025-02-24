using MedCore.Persistence.Interfaces.medical;
using MedCore.Persistence.Repositories.medical;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.medical
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtiesRepository _specialtiesRepository;
        private readonly ILogger<SpecialtiesController> _logger;
        private readonly IConfiguration _configuration;
        public SpecialtiesController(ISpecialtiesRepository specialtiesRepository, ILogger<SpecialtiesController> logger, IConfiguration configuration)
        {
            _specialtiesRepository = specialtiesRepository;
            _logger = logger;
            _configuration = configuration;
        }

        // GET: api/<SpecialtiesController>
        [HttpGet("GetSpecialties")]
        public async Task<IActionResult> Get()
        {
            var specialtiesRepository = await _specialtiesRepository.GetAllAsync();
            return Ok();
        }

        // GET api/<SpecialtiesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SpecialtiesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SpecialtiesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SpecialtiesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
