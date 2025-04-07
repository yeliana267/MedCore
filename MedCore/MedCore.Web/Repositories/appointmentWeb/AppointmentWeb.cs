using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Domain.Base;
using MedCore.Web.Interfaces;
using MedCore.Web.Interfaces.appointments;
using MedCore.Web.Models.appointments;
using MedCore.Web.Repositories;
using MedCore.Web.Repositories.appointmentWeb;

namespace MedCore.Web.Repositories.appointments
{
    public class AppointmentWeb : IAppointmentWeb
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<AppointmentWeb> _logger;

        public AppointmentWeb(IApiClient apiClient, ILogger<AppointmentWeb> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<List<AppointmentModel>> GetAllAsync()
        {
            try
            {
                var appointments = await _apiClient.GetAsync<List<AppointmentModel>>("Appointments/GetAppointments");
                return appointments ?? new List<AppointmentModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las citas");
                throw;
            }
        }


        public async Task<AppointmentModel> GetByIdAsync(int id)
        {
            try
            {
     

                return await _apiClient.GetAsync<AppointmentModel>($"Appointments/GetAppointmentsById?Id={id}")
                    ?? new AppointmentModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener cita con ID {id}");
                throw;
            }
        }

        public async Task<EditAppointmentModel> GetEditModelByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetAsync<EditAppointmentModel>($"Appointments/GetAppointmentsById?Id={id}")
                    ?? new EditAppointmentModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener modelo de edición para cita con ID {id}");
                throw;
            }
        }

        public async Task<bool> CreateAsync(CreateAppointmentModel model)
        {
            try
            {
                var dto = AppointmentsFactory.CreateSaveDto(model);

                var result = await _apiClient.PostAsync<SaveAppointmentsDto, OperationResult>(
                    "Appointments/SaveAppointment", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear cita");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, EditAppointmentModel model)
        {
            try
            {
                var dto = AppointmentsFactory.CreateUpdateDto(model);

                var result = await _apiClient.PutAsync<UpdateAppointmentsDto, OperationResult>(
                    $"Appointments/UpdateAppointment/{id}", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar cita con ID {id}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var dto = new RemoveAppointmentsDto { AppointmentID = id };
                var result = await _apiClient.DeleteAsync<RemoveAppointmentsDto, OperationResult>(
                    "Appointments/DeleteAppointment", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar cita con ID {id}");
                throw;
            }
        }
    }
}
