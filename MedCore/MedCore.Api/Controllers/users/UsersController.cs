using MedCore.Application.Dtos.users.Users;
using MedCore.Application.Interfaces.users;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Interfaces.users;
using Microsoft.AspNetCore.Mvc;


namespace MedCore.Api.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public readonly ILogger<UsersController> _logger;
        public readonly IConfiguration _configuration;
        public UsersController(IUsersService usersService, ILogger<UsersController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _usersService = usersService;
            _logger = logger;

        }


        // GET: api/<UsersController>
        [HttpGet("GetUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await _usersService.GetAll();
            return Ok(users);
        }


        // GET api/<UsersController>/5
        [HttpGet("GetUsersByID")]
        public async Task<IActionResult> Get(int id)
        {
            var users = await _usersService.GetById(id);
            return Ok(users);
        }

        // POST api/<UsersController>
        [HttpPost("SaveUsers")]
        public async Task<IActionResult> Post([FromBody] SaveUsersDto users)
        {
            var user = await _usersService.Save(users);
            return Ok(user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("UpdateUsers")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUsersDto users)
        {
            var user = await _usersService.Update(users);
            return Ok(user);
        }

        // DELETE api/<UsersController>/5
        [HttpPost("DeleteUsers")]
        public async Task<IActionResult> Delete([FromBody] RemoveUsersDto id)
        {
            var user = await _usersService.Remove(id);
            return Ok(user);
        }

    }
}
