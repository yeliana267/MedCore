using MedCore.Domain.Entities.users;
using MedCore.Persistence.Interfaces.users;
using MedCore.Persistence.Repositories.users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsRepository _patientsRepository;
        public readonly ILogger<PatientsController> _logger;
        public readonly IConfiguration _configuration;
        public PatientsController(IPatientsRepository patientsRepository, ILogger<PatientsController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _patientsRepository = patientsRepository;
            _logger = logger;
        }

        // GET: api/<PatientsController>
        [HttpGet("GetPatients")]
        public async Task<IActionResult> Get()
        {
            var patients = await _patientsRepository.GetAllAsync();
            return Ok(patients);
        }

        // GET api/<PatientsController>/5
        [HttpGet("GetPatientByID")]
        public async Task<IActionResult> Get(int id)
        {
            var patient = await _patientsRepository.GetEntityByIdAsync(id);
            return Ok(patient);
        }

        // POST api/<PatientsController>
        [HttpPost("Save")]
        public async Task<IActionResult> Post([FromBody] Patients patients)
        {
            var patient = await _patientsRepository.SaveEntityAsync(patients);
            return Ok(patient);
        }

        // PUT api/<PatientsController>/5
        [HttpPut("Update")]
        public async Task<IActionResult> Put(int id, [FromBody] Patients patients)
        {
            var patient = await _patientsRepository.UpdateEntityAsync(id, patients);
            return Ok(patient);
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _patientsRepository.DeleteEntityByIdAsync(id);
            return Ok(patient);
        }
    }
}
