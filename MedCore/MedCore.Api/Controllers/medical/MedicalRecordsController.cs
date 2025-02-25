using MedCore.Domain.Entities.medical;
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
            return Ok(medicalRecords);
        }

        // POST api/<MedicalRecordsController>
        [HttpPost("SaveMedicalRecords")]
        public async Task<IActionResult> Post([FromBody] MedicalRecords medicalRecord)
        {
            var medicalRecords = await _medicalRecordsRepository.SaveEntityAsync(medicalRecord);
            return Ok(medicalRecord);
        }

        // PUT api/<MedicalRecordsController>/5
        [HttpPut("UpdateMedicalRecords")]
        public async Task<IActionResult> Put(short id, [FromBody] MedicalRecords medicalRecord)
        {
            var medicalRecords = await _medicalRecordsRepository.UpdateEntityAsync(medicalRecord);
            return NoContent();
        }
        
        // DELETE api/<MedicalRecordsController>/5
        [HttpDelete("DeleteMedicalRecords")]
        public async Task<IActionResult> Delete(int id)
        {
            var medicalRecord = await _medicalRecordsRepository.DeleteMedicalRecordAsync(id);
            return Ok();
        }
        
        // GET api/<MedicalRecordsController>/5
        [HttpGet("GetMedicalRecordsByPatientIdAsync")]
        public async Task<IActionResult> Get(int patientId)
        {
            var medicalRecords = await _medicalRecordsRepository.GetMedicalRecordsByPatientIdAsync(patientId);
            return Ok(medicalRecords);
        }

        // GET api/<MedicalRecordsController>/5
        [HttpGet("GetMedicalRecordsByDateRangeAsync")]
        public async Task<IActionResult> Get(DateTime startDate, DateTime endDate)
        {
            var medicalRecords = await _medicalRecordsRepository.GetMedicalRecordsByDateRangeAsync(startDate, endDate);
            return Ok(medicalRecords);
        }

    }
}
