using MedCore.Persistence.Interfaces.appointments;
using MedCore.Persistence.Interfaces.System;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.system
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
      
            private readonly INotificationsRepository _notificationsRepository;
            public readonly ILogger<NotificationsController> _logger;
            public readonly IConfiguration _configuration;
            public NotificationsController(INotificationsRepository notificationsRepository, 
                ILogger<NotificationsController> logger, IConfiguration configuration)
            {
            _notificationsRepository = notificationsRepository;
            _logger = logger;
                _configuration = configuration;
            }




            // GET: api/<NotificationsController>
            [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<NotificationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NotificationsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NotificationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotificationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
