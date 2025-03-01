using MedCore.Persistence.Interfaces.appointments;
using MedCore.Persistence.Interfaces.Insurance;
using MedCore.Persistence.Repositories.appointments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.Insurance
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceProvidersController : ControllerBase
    {
        public readonly IInsuranceProvidersRepository _insuranceProvidersRepository;
        public readonly ILogger<InsuranceProvidersController> _logger;
        public readonly IConfiguration _configuration;
       
      
       public InsuranceProvidersController(IInsuranceProvidersRepository insuranceProvidersRepository,IConfiguration configuration, ILogger<InsuranceProvidersController> logger) 
        {
            _logger = logger;
            _configuration = configuration;
            _insuranceProvidersRepository = insuranceProvidersRepository;
           
            
        }
        // GET: api/<InsuranceProvidersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<InsuranceProvidersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InsuranceProvidersController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<InsuranceProvidersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<InsuranceProvidersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
