using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Context;
using MedCore.Persistence.Repositories.appointments;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace MedCore.Persistence.Test.appointments
{
    public class UnitTestAppointments
    {
        private readonly MedCoreContext _context;
        private readonly IAppointmentsRepository _repository;

        public UnitTestAppointments()
        {
            

            var mockLogger = new Mock<ILogger<AppointmentsRepository>>();
            var mockConfig = new Mock<IConfiguration>();

            _repository = new AppointmentsRepository(_context, mockLogger.Object, mockConfig.Object);
        }

        [Fact]
        public async Task SaveEntityAsync_ValidAppointment_ReturnsSuccess()
        {
            // Arrange
            var appointment = new Appointments
            {
                DoctorID = 1,
                PatientID = 2,
                AppointmentDate = DateTime.Now.AddDays(2),
                StatusID = 1
            };

            // Act
            var result = await _repository.SaveEntityAsync(appointment);

            // Assert
            Assert.True(result.Success);
            Assert.Contains("guardada", result.Message.ToLower());
        }

        [Fact]
        public async Task GetEntityByIdAsync_ReturnsCorrectAppointment()
        {
            // Arrange
            var appointment = new Appointments
            {
                DoctorID = 3,
                PatientID = 4,
                AppointmentDate = DateTime.Now.AddDays(1),
                StatusID = 2
            };

            await _repository.SaveEntityAsync(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetEntityByIdAsync(appointment.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.DoctorID);
        }

        [Fact]
        public async Task DeleteEntityByIdAsync_DeletesSuccessfully()
        {
            // Arrange
            var appointment = new Appointments
            {
                DoctorID = 5,
                PatientID = 6,
                AppointmentDate = DateTime.Now.AddDays(3),
                StatusID = 1
            };

            await _repository.SaveEntityAsync(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteEntityByIdAsync(appointment.Id);

            // Assert
            Assert.True(result.Success);
            Assert.Contains("eliminada", result.Message.ToLower());
        }
    }
}
