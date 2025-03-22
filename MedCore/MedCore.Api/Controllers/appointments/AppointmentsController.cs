using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Application.Interfaces.appointments;
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
        private readonly IAppointmentsService _appointmentsService;
        public readonly ILogger<AppointmentsController> _logger;
        public readonly IConfiguration _configuration;
        public AppointmentsController(IAppointmentsService appointmentsService, ILogger<AppointmentsController> logger, IConfiguration configuration)
        {
            _appointmentsService = appointmentsService;
            _logger = logger;
            _configuration = configuration;
        }
        // GET: api/<AppointmentsController>
        [HttpGet("GetAppointments")]
        public async Task<IActionResult> Get()
        {
            var appointments = await _appointmentsService.GetAll();
            return Ok(appointments);
        }

        // GET api/<AppointmentsController>/5
        [HttpGet("GetAppointmentsById")]
        public async Task<IActionResult> GetAppointmentsByDoctorIdAsync(int Id)
        {
            var appointments = await _appointmentsService.GetAppointmentsByDoctorIdAsync(Id);
            return Ok(appointments);
        }
        [HttpGet("GetPending")]
        public async Task<IActionResult> GetPendingAppointmentsAsync()
        {
            var appointments = await _appointmentsService.GetPendingAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpGet("GetAppointmentsByPatientId")]
        public async Task<IActionResult> GetAppointmentsByPatientIdAsync(int patientId)
        {
            var appointments = await _appointmentsService.GetAppointmentsByPatientIdAsync(patientId);
            return Ok(appointments);
        }

        [HttpGet("GetAppointmentsByDoctorId")]
        public async Task<IActionResult> Get(int doctorId)
        {
            var appointments = await _appointmentsService.GetAppointmentsByDoctorIdAsync(doctorId);
            return Ok(appointments);
        }


        // POST api/<AppointmentsController>
        [HttpPost("SaveAppointment")]
        public async Task<IActionResult> Post([FromBody] SaveAppointmentsDto appointmentsDto)
        {
            var appointment = await _appointmentsService.Save(appointmentsDto);
            return Ok(appointment);
        }

        // PUT api/<AppointmentsController>/5
        [HttpPut("UpdateAppointment")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateAppointmentsDto appointmentsDto)
        {
            var appointment = await _appointmentsService.Update(appointmentsDto);
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
        public async Task<IActionResult> Delete(RemoveAppointmentsDto appointmentsDto)
        {
            var appointment = await _appointmentsService.Remove(appointmentsDto);
        
                return Ok(appointment);
            
        }

    }
}
