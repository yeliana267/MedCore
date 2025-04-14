using MedCore.Application.Dtos.Medical.AvailibilityModesDto;
using MedCore.Domain.Base;
using MedCore.Web.Interfaces;
using MedCore.Web.Interfaces.Medical;
using MedCore.Web.Models.Medical.AvailabilityModes;
using AvailabilityModesModel = MedCore.Model.Models.medical.AvailabilityModesModel;

namespace MedCore.Web.Repositories.Medical
{
    public class AvailabilityModesWeb : IAvailabilityModesWeb
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<AvailabilityModesWeb> _logger;

        public AvailabilityModesWeb(IApiClient apiClient, ILogger<AvailabilityModesWeb> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<List<AvailabilityModesModel>> GetAllAsync()
        {
            try
            {
                var availabilityModes = await _apiClient.GetAsync<List<AvailabilityModesModel>>("AvailabilityModes/GetAvailability");
                return availabilityModes ?? new List<AvailabilityModesModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los modos de disponibilidad");
                throw;
            }
        }

        public async Task<AvailabilityModesModel> GetByIdAsync(short id)
        {
            try
            {
                return await _apiClient.GetAsync<AvailabilityModesModel>($"AvailabilityModes/GetAvailabilityById?id={id}")
                    ?? new AvailabilityModesModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener modo de disponibilidad con ID {id}");
                throw;
            }
        }

        public async Task<EditAvailabilityModesModel> GetEditModelByIdAsync(short id)
        {
            try
            {
                var model = await _apiClient.GetAsync<EditAvailabilityModesModel>($"AvailabilityModes/GetAvailabilityById?id={id}");
                return model ?? new EditAvailabilityModesModel { AvailabilityMode = string.Empty };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener modelo de edición para modo de disponibilidad con ID {id}");
                throw;
            }
        }

        public async Task<bool> CreateAsync(CreateAvailabilityModesModel model)
        {
            try
            {
                var dto = AvailabilityModesFactory.CreateSaveDto(model);

                var result = await _apiClient.PostAsync<SaveAvailabilityModesDto, OperationResult>(
                    "AvailabilityModes/SaveAvailability", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear modo de disponibilidad");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(short id, EditAvailabilityModesModel model)
        {
            try
            {
                var dto = AvailabilityModesFactory.CreateUpdateDto(model);
                dto.AvailabilityModesId = id;
                var result = await _apiClient.PutAsync<UpdateAvailabilityModesDto, OperationResult>(
                    "AvailabilityModes/UpdateAvailability", dto);
                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar modo de disponibilidad con ID {id}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(short id)
        {
            try
            {
                var dto = new RemoveAvailabilityModesDto { AvailabilityModesId = id };
                var result = await _apiClient.DeleteAsync<RemoveAvailabilityModesDto, OperationResult>(
                    "AvailabilityModes/DeleteAvailability", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar modo de disponibilidad con ID {id}");
                throw;
            }
        }
    }
}
