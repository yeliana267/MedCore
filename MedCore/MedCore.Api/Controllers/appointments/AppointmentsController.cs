using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedCore.Api.Controllers.appointments
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        public readonly ILogger<AppointmentsController> _logger;
        public readonly IConfiguration _configuration;
        public AppointmentsController(IAppointmentsRepository appointmentsRepository, ILogger<AppointmentsController> logger, IConfiguration configuration)
        {
            _appointmentsRepository = appointmentsRepository;
            _logger = logger;
            _configuration = configuration;
        }
        // GET: api/<AppointmentsController>
        [HttpGet("GetAppointments")]
        public async Task<IActionResult> Get()
        {
            var appointments = await _appointmentsRepository.GetAllAsync();
            return Ok(appointments);
        }

        // GET api/<AppointmentsController>/5
        [HttpGet("GetAppointmentsById")]
        public async Task<IActionResult> Get(int id)
        {
            var appointments = await _appointmentsRepository.GetEntityByIdAsync(id);
            return Ok(appointments);
        }

        // POST api/<AppointmentsController>
        [HttpPost("SaveAppointment")]
        public async Task<IActionResult> Post([FromBody] Appointments appointments)
        {
            var appointment = await _appointmentsRepository.SaveEntityAsync(appointments);
            return Ok(appointment);
        }

        // PUT api/<AppointmentsController>/5
        [HttpPut("UpdateAppointment")]
        public async Task<IActionResult> Put(int id, [FromBody] Appointments appointments)
        {
            var appointment = await _appointmentsRepository.UpdateEntityAsync(id, appointments);
            if (id == null)
            {
                return NotFound("Ingrese el Id de la cita que quiere actualizar");
            }
            else
            {
                return Ok(appointment);
            }
        }

        // DELETE api/<AppointmentsController>/5
        [HttpDelete("DeleteAppointment")]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _appointmentsRepository.DeleteEntityByIdAsync(id);
            if (id == null)
            {
                return NotFound("Ingrese el Id de la cita que quiere borrar");
            }
            else
            {
                return Ok(appointment);
            }
        }

    }
}
