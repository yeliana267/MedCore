using MedCore.Application.Dtos.Medical.SpecialtiesDto;
using MedCore.Domain.Base;
using MedCore.Web.Interfaces;
using MedCore.Web.Interfaces.Medical;
using MedCore.Web.Models.Medical.SpecialtiesModels;
using SpecialtiesModel = MedCore.Model.Models.medical.SpecialtiesModel;


namespace MedCore.Web.Repositories.Medical
{
    public class SpecialtiesWeb : ISpecialtiesWeb
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<SpecialtiesWeb> _logger;

        public SpecialtiesWeb(IApiClient apiClient, ILogger<SpecialtiesWeb> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<List<SpecialtiesModel>> GetAllAsync()
        {
            try
            {
                var specialties = await _apiClient.GetAsync<List<SpecialtiesModel>>("Specialties/GetSpecialties");
                return specialties ?? new List<SpecialtiesModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las especialidades");
                throw;
            }
        }

        public async Task<SpecialtiesModel> GetByIdAsync(short id)
        {
            try
            {
                return await _apiClient.GetAsync<SpecialtiesModel>($"Specialties/GetSpecialtyById?id={id}")
                    ?? new SpecialtiesModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener especialidad con ID {id}");
                throw;
            }
        }

        public async Task<EditSpecialtiesModel> GetEditModelByIdAsync(short id)
        {
            try
            {
                return await _apiClient.GetAsync<EditSpecialtiesModel>($"Specialties/GetSpecialtyById?id={id}")
                    ?? new EditSpecialtiesModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener modelo de edición para especialidad con ID {id}");
                throw;
            }
        }

        public async Task<bool> CreateAsync(CreateSpecialtiesModel model)
        {
            try
            {
                var dto = SpecialtiesFactory.CreateSaveDto(model);

                var result = await _apiClient.PostAsync<SaveSpecialtiesDto, OperationResult>(
                    "Specialties/SaveSpecialty", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear especialidad");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(short id, EditSpecialtiesModel model)
        {
            try
            {
                var dto = SpecialtiesFactory.CreateUpdateDto(model);

                var result = await _apiClient.PutAsync<UpdateSpecialtiesDto, OperationResult>(
                    $"Specialties/UpdateSpecialty/{id}", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar especialidad con ID {id}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(short id)
        {
            try
            {
                var dto = new RemoveSpecialtiesDto { SpecialtiesId = id };
                var result = await _apiClient.DeleteAsync<RemoveSpecialtiesDto, OperationResult>(
                    "Specialties/DeleteSpecialty", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar especialidad con ID {id}");
                throw;
            }
        }
    }
}