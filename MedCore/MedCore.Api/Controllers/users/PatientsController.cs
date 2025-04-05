using MedCore.Application.Dtos.users.Patients;
using MedCore.Application.Interfaces.users;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Interfaces.users;
using MedCore.Persistence.Repositories.users;
using Microsoft.AspNetCore.Mvc;


namespace MedCore.Api.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;
        public readonly ILogger<PatientsController> _logger;
        public readonly IConfiguration _configuration;
        public PatientsController(IPatientsService patientsService, ILogger<PatientsController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _patientsService = patientsService;
            _logger = logger;
        }

        // GET: api/<PatientsController>
        [HttpGet("GetPatients")]
        public async Task<IActionResult> Get()
        {
            var patients = await _patientsService.GetAll();
            return Ok(patients);
        }

        // GET api/<PatientsController>/5
        [HttpGet("GetPatientByID")]
        public async Task<IActionResult> Get(int id)
        {
            var patient = await _patientsService.GetById(id);
            return Ok(patient);
        }

        // POST api/<PatientsController>
        [HttpPost("SavePatients")]
        public async Task<IActionResult> SavePatient ([FromBody] SavePatientsDto dto)
        {
            var patient = await _patientsService.Save(dto);
            return Ok(patient);
        }
            
        // PUT api/<PatientsController>/5
        [HttpPut("UpdatePatients")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePatientsDto patients)
        {
            var patient = await _patientsService.Update(patients);
            return Ok(patient);
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("DeletePatients")]
        public async Task<IActionResult> Delete(RemovePatientsDto id)
        {
            var patient = await _patientsService.Remove(id);
            return Ok(patient);
        }
    }
}
