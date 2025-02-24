using MedCore.Persistence.Interfaces.medical;
using MedCore.Persistence.Repositories.medical;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.medical
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordsRepository _medicalRecordsRepository;
        private readonly ILogger<MedicalRecordsController> _logger;
        private readonly IConfiguration _configuration;
        public MedicalRecordsController(IMedicalRecordsRepository medicalRecordsRepository, ILogger<MedicalRecordsController> logger, IConfiguration configuration)
        {
            _medicalRecordsRepository = medicalRecordsRepository;
            _logger = logger;
            _configuration = configuration;
        }

        // GET: api/<MedicalRecordsController>
        [HttpGet("GetMedicalRecords")]
        public async Task<IActionResult> Get()
        {
            var medicalRecords = await _medicalRecordsRepository.GetAllAsync();
            return Ok();
        }

        // GET api/<MedicalRecordsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MedicalRecordsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MedicalRecordsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MedicalRecordsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
