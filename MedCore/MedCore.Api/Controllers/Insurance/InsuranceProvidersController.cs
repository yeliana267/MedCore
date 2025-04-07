using MedCore.Application.Dtos.Insurance.InsuranceProvider;
using MedCore.Application.Dtos.Insurance.InsuranceProviders;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Persistence.Interfaces.appointments;
using MedCore.Persistence.Interfaces.Insurance;
using MedCore.Persistence.Repositories.appointments;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.Insurance
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceProvidersController : ControllerBase
    {
        public readonly IInsuranceProvidersService _insuranceProvidersService;
        public readonly ILogger<InsuranceProvidersController> _logger;
        public readonly IConfiguration _configuration;
       
      
       public InsuranceProvidersController(IInsuranceProvidersService insuranceProvidersService,
                                           IConfiguration configuration, ILogger<InsuranceProvidersController> logger) 
        {
            _logger = logger;
            _configuration = configuration;
            _insuranceProvidersService = insuranceProvidersService;
           
            
        }
        // GET: api/<InsuranceProvidersController>
        [HttpGet("GetInsuranceProviders")]
        public async Task<IActionResult> Get()
        {
            var insuranceProviders = await _insuranceProvidersService.GetAll();
            return Ok(insuranceProviders);
        }

        // GET api/<InsuranceProvidersController>/5
        [HttpGet("GetInsuranceProvidersById")]
        public async Task<IActionResult> Get(int id)
            
        {
            var insuranceProviders = await _insuranceProvidersService.GetById(id);
            return Ok(insuranceProviders);
        }

        // POST api/<InsuranceProvidersController>
        [HttpPost("SaveInsuranceProviders")]
        public async Task<IActionResult> Post([FromBody] SaveInsuranceProvidersDto insuranceProviders)
        {
            var insuranceProvider = await _insuranceProvidersService.Save(insuranceProviders);
            return Ok(insuranceProvider);
        }

        // PUT api/<InsuranceProvidersController>/5
        [HttpPut("UpdateInsuranceProviders")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateInsuranceProvidersDto insuranceProviders)
        {
            var insuranceProvider = await _insuranceProvidersService.Update(insuranceProviders);
            return Ok(insuranceProvider);

        }

        // DELETE api/<InsuranceProvidersController>/5
        [HttpDelete("DeleteInsuranceProviders")]
        public async Task<IActionResult> Delete(RemoveInsuranceProvidersDto id)
        {
            var insuranceProvider = await _insuranceProvidersService.Remove(id);
            return Ok(insuranceProvider);
        }
    }
}
