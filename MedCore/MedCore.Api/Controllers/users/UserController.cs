using MedCore.Domain.Entities.users;
using MedCore.Persistence.Interfaces.users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly  IUsersRepository _usersRepository;
        public readonly ILogger<UserController> _logger;
        public readonly IConfiguration _configuration;
        public UserController(IUsersRepository usersRepository, ILogger<UserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _usersRepository = usersRepository;

        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _usersRepository.GetAllAsync();
            return Ok(user);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("Update")]
        public async Task<IActionResult> Put(int id, [FromBody] Users users)
        {
            var user = await _usersRepository.UpdateEntityAsync(id, users);
            return Ok(user);
        }
      
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
