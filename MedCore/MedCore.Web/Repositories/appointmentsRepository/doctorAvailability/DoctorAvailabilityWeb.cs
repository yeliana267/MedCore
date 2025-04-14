
using MedCore.Web.Interfaces;
using MedCore.Web.Interfaces.appointments.doctorAvailability;
using MedCore.Web.Models.doctorAvailability;

using MedCore.Domain.Base;

using MedCore.Application.Dtos.appointments.DoctorAvailability;

namespace MedCore.Web.Repositories.appointmentsRepository.doctorAvailability
{
    public class DoctorAvailabilityWeb : IDoctorAvailabilityWeb
    {

            private readonly IApiClient _apiClient;
            private readonly ILogger<DoctorAvailabilityWeb> _logger;

            public DoctorAvailabilityWeb(IApiClient apiClient, ILogger<DoctorAvailabilityWeb> logger)
            {
                _apiClient = apiClient;
                _logger = logger;
            }

            public async Task<bool> CreateAsync(CreateDoctorAvailabilityModel model)
            {
            try
            {
                var dto = DoctorAvailabilityFactory.CreateSaveDto(model);

                var result = await _apiClient.PostAsync<SaveDoctorAvailabilityDto, OperationResult>(
                    "DoctorAvailability/SaveDoctorAvailability", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var dto = new RemoveDoctorAvailabilityDto { AvailabilityID = id };
                var result = await _apiClient.DeleteAsync<RemoveDoctorAvailabilityDto, OperationResult>(
                    "DoctorAvailability/DeleteDoctorAvailability", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar con ID {id}");
                throw;
            }
        }
        public async Task<List<DoctorAvailabilityModel>> GetAllAsync()
        {
            try
            {
                var DoctorAvailability = await _apiClient.GetAsync<List<DoctorAvailabilityModel>>("DoctorAvailability/GetDoctorAvailability");
                return DoctorAvailability ?? new List<DoctorAvailabilityModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todo");
                throw;
            }
        }

        public async Task<DoctorAvailabilityModel> GetByIdAsync(int id)
        {
            try
            {


                return await _apiClient.GetAsync<DoctorAvailabilityModel>($"DoctorAvailability/GetDoctorAvailabilityById?id={id}")
                    ?? new DoctorAvailabilityModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener con ID {id}");
                throw;
            }
        }

        public async Task<EditDoctorAvailabilityModel> GetEditModelByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetAsync<EditDoctorAvailabilityModel>($"DoctorAvailability/GetDoctorAvailabilityById?id={id}")
                    ?? new EditDoctorAvailabilityModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener modelo de edición con ID {id}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, EditDoctorAvailabilityModel model)
        {
            try
            {
                var dto = DoctorAvailabilityFactory.CreateUpdateDto(model);

                var result = await _apiClient.PutAsync<UpdateDoctorAvailabilityDto, OperationResult>(
                    $"DoctorAvailability/UpdateDoctorAvailability", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar con ID {id}");
                throw;
            }
        }
    }

}