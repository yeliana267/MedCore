﻿using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.system;
using MedCore.Persistence.Interfaces.appointments;
using MedCore.Persistence.Repositories.appointments;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var appointments = await _appointmentsRepository.GetAppointmentsByPatientIdAsync(id);
            return Ok(appointments);
        }

        // POST api/<AppointmentsController>
        [HttpPost("SaveAppointments")]
        public async Task<IActionResult> Post([FromBody] Appointments appointments)
        {
            var appointment = await _appointmentsRepository.SaveEntityAsync(appointments);
            return Ok(appointments);
        }


            // PUT api/<AppointmentsController>/5
            [HttpPut("UpdateAppointments")]
        public async Task<IActionResult> Put(int id, [FromBody] Appointments appointments)
        {
            var appointment = await _appointmentsRepository.UpdateEntityAsync(id, appointments);
            return Ok(appointments);
        }

        // DELETE api/<AppointmentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
