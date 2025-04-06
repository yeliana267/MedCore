using MedCore.Application.Dtos.Medical.MedicalRecordsDto;
using MedCore.Application.Interfaces.Medical;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.medical
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordsService _medicalRecordsServices;
        private readonly ILogger<MedicalRecordsController> _logger;
        private readonly IConfiguration _configuration;
        public MedicalRecordsController(IMedicalRecordsService medicalRecordsService, ILogger<MedicalRecordsController> logger, IConfiguration configuration)
        {
            _medicalRecordsServices = medicalRecordsService;
            _logger = logger;
            _configuration = configuration;
        }

        // GET: api/<MedicalRecordsController>
        [HttpGet("GetMedicalRecords")]
        public async Task<IActionResult> Get()
        {
            var medicalRecords = await _medicalRecordsServices.GetAll();
            return Ok(medicalRecords);
        }

        // GET api/<MedicalRecordsController>/5
        [HttpGet("GetMedicalRecordsById")]
        public async Task<IActionResult> Get(int id)
        {
            var medicalRecords = await _medicalRecordsServices.GetById(id); 
            return Ok(medicalRecords);
        }


        // POST api/<MedicalRecordsController>
        [HttpPost("SaveMedicalRecords")]
        public async Task<IActionResult> Post([FromBody] SaveMedicalRecordsDto medicalRecordDto)
        {
            var medicalRecords = await _medicalRecordsServices.Save(medicalRecordDto);
            return Ok(medicalRecords);
        }

        // PUT api/<MedicalRecordsController>/5
        [HttpPut("UpdateMedicalRecords")]
        public async Task<IActionResult> Put(short id, [FromBody] UpdateMedicalRecordsDto medicalRecordDto)
        {
            var medicalRecords = await _medicalRecordsServices.Update(medicalRecordDto); //falla
            return Ok(medicalRecords);
        }
        
        // DELETE api/<MedicalRecordsController>/5
        [HttpDelete("DeleteMedicalRecords")]
        public async Task<IActionResult> Delete(RemoveMedicalRecordsDto medicalRecordsDto)
        {
            var medicalRecords = await _medicalRecordsServices.Remove(medicalRecordsDto);
            return Ok(medicalRecords);
        }

    }
}
