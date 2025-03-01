using MedCore.Domain.Entities.users;
using MedCore.Persistence.Interfaces.users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        public readonly ILogger<UsersController> _logger;
        public readonly IConfiguration _configuration;
        public UsersController(IUsersRepository usersRepository, ILogger<UsersController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _usersRepository = usersRepository;
            _logger = logger;

        }


        // GET: api/<UsersController>
        [HttpGet("GetUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await _usersRepository.GetAllAsync();
            return Ok(users);
        }


        // GET api/<UsersController>/5
        [HttpGet("GetUsersByID")]
        public async Task<IActionResult> Get(int id)
        {
            var users = await _usersRepository.GetEntityByIdAsync(id);
            return Ok(users);
        }

        // POST api/<UsersController>
        [HttpPost("Save")]
        public async Task<IActionResult> Post([FromBody] Users users)
        {
            var user = await _usersRepository.SaveEntityAsync(users);
            return Ok(user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("Update")]
        public async Task<IActionResult> Put(int id, [FromBody] Users users)
        {
            var user = await _usersRepository.UpdateUserAsync(id, users);
            return Ok(user);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _usersRepository.DeleteUsersByIdAsync(id);
            return Ok(user);    
        }
    }
}
