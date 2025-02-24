using MedCore.Persistence.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        public readonly IBaseUsers _baseUsers;
        public readonly ILogger<DoctorController> _logger;
        public readonly IConfiguration _configuration;
        public DoctorController(IBaseUsers baseUsers, ILogger <DoctorController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _baseUsers = baseUsers;
        }   


        // GET: api/<DoctorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DoctorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoctorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
