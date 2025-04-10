﻿using MedCore.Application.Dtos.appointments.Appointments;
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
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentsService.GetAll();
            return Ok(appointments);
        }

        // GET api/<AppointmentsController>/5
        [HttpGet("GetAppointmentsById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var appointments = await _appointmentsService.GetById(Id);
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
        [HttpPut("UpdateAppointment/{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateAppointmentsDto updateAppointmentsDto)
        {
            var appointment = await _appointmentsService.Update(updateAppointmentsDto);
            return Ok(appointment);
        }


        // DELETE api/<AppointmentsController>/5
        [HttpPost("DeleteAppointmentById")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var dto = new RemoveAppointmentsDto { AppointmentID = id };
            var result = await _appointmentsService.Remove(dto);
            return Ok(result);
        }


    }
}
