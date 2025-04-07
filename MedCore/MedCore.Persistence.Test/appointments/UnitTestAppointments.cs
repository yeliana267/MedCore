using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Context;
using MedCore.Persistence.Repositories.appointments;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Moq;


namespace MedCore.Persistence.Test.appointments
{
    public class UnitTestAppointments
    {
        private readonly MedCoreContext _context;
        private readonly IAppointmentsRepository _repository;

        public UnitTestAppointments()
        {
            var options = new DbContextOptionsBuilder<MedCoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var mockLogger = new Mock<ILogger<AppointmentsRepository>>();
            var mockConfig = new Mock<IConfiguration>();

            _context = new MedCoreContext(options);
            _repository = new AppointmentsRepository(_context, mockLogger.Object, mockConfig.Object);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldSaveAppointmentSuccessfully()
        {
            var appointment = new Appointments
            {
                DoctorID = 1,
                PatientID = 2,
                AppointmentDate = DateTime.Now.AddDays(1),
                StatusID = 1
            };

            var result = await _repository.SaveEntityAsync(appointment);

            Assert.True(result.Success);
            Assert.Contains("guardada", result.Message.ToLower());
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnCorrectAppointment()
        {
            var appointment = new Appointments
            {
                DoctorID = 3,
                PatientID = 4,
                AppointmentDate = DateTime.Now.AddDays(1),
                StatusID = 1
            };

            await _repository.SaveEntityAsync(appointment);
            await _context.SaveChangesAsync();

            var result = await _repository.GetEntityByIdAsync(appointment.Id);

            Assert.NotNull(result);
            Assert.Equal(3, result.DoctorID);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldUpdateSuccessfully()
        {
            var appointment = new Appointments
            {
                DoctorID = 5,
                PatientID = 6,
                AppointmentDate = DateTime.Now.AddDays(2),
                StatusID = 1
            };

            await _repository.SaveEntityAsync(appointment);
            await _context.SaveChangesAsync();

            appointment.StatusID = 2;
            var result = await _repository.UpdateEntityAsync(appointment.Id, appointment);

            Assert.True(result.Success);
            Assert.Contains("actualizada", result.Message.ToLower());
        }

        [Fact]
        public async Task DeleteEntityByIdAsync_ShouldDeleteSuccessfully()
        {
            var appointment = new Appointments
            {
                DoctorID = 7,
                PatientID = 8,
                AppointmentDate = DateTime.Now.AddDays(3),
                StatusID = 1
            };

            await _repository.SaveEntityAsync(appointment);
            await _context.SaveChangesAsync();

            var result = await _repository.DeleteEntityByIdAsync(appointment.Id);

            Assert.True(result.Success);
            Assert.Contains("eliminada", result.Message.ToLower());
        }

        [Fact]
        public async Task GetAppointmentsByDoctorIdAsync_ShouldReturnAppointments()
        {
            var appointment = new Appointments
            {
                DoctorID = 10,
                PatientID = 20,
                AppointmentDate = DateTime.Now.AddDays(1),
                StatusID = 1
            };

            await _repository.SaveEntityAsync(appointment);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAppointmentsByDoctorIdAsync(10);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetAppointmentsByPatientIdAsync_ShouldReturnAppointments()
        {
            var appointment = new Appointments
            {
                DoctorID = 30,
                PatientID = 40,
                AppointmentDate = DateTime.Now.AddDays(1),
                StatusID = 1
            };

            await _repository.SaveEntityAsync(appointment);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAppointmentsByPatientIdAsync(40);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetPendingAppointmentsAsync_ShouldReturnPendingAppointments()
        {
            var appointment = new Appointments
            {
                DoctorID = 50,
                PatientID = 60,
                AppointmentDate = DateTime.Now.AddDays(1),
                StatusID = 1 // pendiente
            };

            await _repository.SaveEntityAsync(appointment);
            await _context.SaveChangesAsync();

            var result = await _repository.GetPendingAppointmentsAsync();

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetAppointmentsByDateAsync_ShouldReturnAppointmentsForDate()
        {
            var date = DateTime.Today.AddDays(1);
            var appointment = new Appointments
            {
                DoctorID = 70,
                PatientID = 80,
                AppointmentDate = date,
                StatusID = 1
            };

            await _repository.SaveEntityAsync(appointment);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAppointmentsByDateAsync(date);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }
    }
}
