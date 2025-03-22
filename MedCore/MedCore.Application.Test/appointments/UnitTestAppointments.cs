using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Persistence.Interfaces.appointments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MedCore.Application.Test.appointments
{
    public class AppointmentsServiceTests
    {
        private readonly Mock<IAppointmentsRepository> _mockRepo;
        private readonly Mock<ILogger<AppointmentsService>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly AppointmentsService _service;

        public AppointmentsServiceTests()
        {
            _mockRepo = new Mock<IAppointmentsRepository>();
            _mockLogger = new Mock<ILogger<AppointmentsService>>();
            _mockConfig = new Mock<IConfiguration>();
            _service = new AppointmentsService(_mockRepo.Object, _mockLogger.Object, _mockConfig.Object);
        }

        [Fact]
        public async Task Save_ValidAppointmentDto_ReturnsSuccess()
        {
            // Arrange
            var dto = new SaveAppointmentsDto
            {
                DoctorID = 1,
                PatientID = 2,
                AppointmentDate = DateTime.Now.AddDays(1),
                StatusID = 1
            };

            _mockRepo.Setup(r => r.SaveEntityAsync(It.IsAny<Appointments>()))
                     .ReturnsAsync(new OperationResult { Success = true, Message = "Guardado con éxito" });

            // Act
            var result = await _service.Save(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Guardado con éxito", result.Message);
        }

        [Fact]
        public async Task Save_NullDto_ReturnsError()
        {
            // Act
            var result = await _service.Save(null);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Los datos de la cita no pueden estar vacíos.", result.Message);
        }

        [Fact]
        public async Task Save_InvalidDoctorOrPatient_ReturnsError()
        {
            // Arrange
            var dto = new SaveAppointmentsDto
            {
                DoctorID = 0,
                PatientID = 0,
                AppointmentDate = DateTime.Now.AddDays(1),
                StatusID = 1
            };

            // Act
            var result = await _service.Save(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El paciente y el doctor deben ser válidos.", result.Message);
        }

        [Fact]
        public async Task Save_PastAppointmentDate_ReturnsError()
        {
            // Arrange
            var dto = new SaveAppointmentsDto
            {
                DoctorID = 1,
                PatientID = 1,
                AppointmentDate = DateTime.Now.AddDays(-1),
                StatusID = 1
            };

            // Act
            var result = await _service.Save(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La fecha de la cita debe ser futura.", result.Message);
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsAppointment()
        {
            // Arrange
            int id = 1;
            var fakeAppointment = new Appointments
            {
                Id = id,
                DoctorID = 1,
                PatientID = 2,
                AppointmentDate = DateTime.Now.AddDays(1),
                StatusID = 1
            };

            _mockRepo.Setup(r => r.GetEntityByIdAsync(id)).ReturnsAsync(fakeAppointment);

            // Act
            var result = await _service.GetById(id);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetById_InvalidId_ReturnsError()
        {
            // Act
            var result = await _service.GetById(0);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El ID proporcionado no es válido.", result.Message);
        }

        [Fact]
        public async Task Remove_ValidDto_ReturnsSuccess()
        {
            // Arrange
            var dto = new RemoveAppointmentsDto { AppointmentID = 1 };

            _mockRepo.Setup(r => r.DeleteEntityByIdAsync(dto.AppointmentID))
                     .ReturnsAsync(new OperationResult { Success = true, Message = "Cita eliminada correctamente." });

            // Act
            var result = await _service.Remove(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Contains("Cita eliminada correctamente", result.Message);
        }
    }
}
