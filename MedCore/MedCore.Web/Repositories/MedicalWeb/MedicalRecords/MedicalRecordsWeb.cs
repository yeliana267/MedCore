using MedCore.Application.Dtos.Medical.MedicalRecordsDto;
using MedCore.Domain.Base;
using MedCore.Web.Interfaces;
using MedCore.Web.Interfaces.Medical;
using MedCore.Web.Models.Medical.MedicalRecordsModels;
using MedicalRecordsModel = MedCore.Model.Models.medical.MedicalRecordsModel;

namespace MedCore.Web.Repositories.Medical
{
    public class MedicalRecordsWeb : IMedicalRecordsWeb
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<MedicalRecordsWeb> _logger;

        public MedicalRecordsWeb(IApiClient apiClient, ILogger<MedicalRecordsWeb> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<List<MedicalRecordsModel>> GetAllAsync()
        {
            try
            {
                var medicalRecords = await _apiClient.GetAsync<List<MedicalRecordsModel>>("MedicalRecords/GetMedicalRecords");
                return medicalRecords ?? new List<MedicalRecordsModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los registros médicos");
                throw;
            }
        }

        public async Task<MedicalRecordsModel> GetByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetAsync<MedicalRecordsModel>($"MedicalRecords/GetMedicalRecordById?id={id}")
                    ?? new MedicalRecordsModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener registro médico con ID {id}");
                throw;
            }
        }

        public async Task<EditMedicalRecordsModel> GetEditModelByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetAsync<EditMedicalRecordsModel>($"MedicalRecords/GetMedicalRecordById?id={id}")
                    ?? new EditMedicalRecordsModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener modelo de edición para registro médico con ID {id}");
                throw;
            }
        }

        public async Task<bool> CreateAsync(CreateMedicalRecordsModel model)
        {
            try
            {
                var dto = MedicalRecordsFactory.CreateSaveDto(model);

                var result = await _apiClient.PostAsync<SaveMedicalRecordsDto, OperationResult>(
                    "MedicalRecords/SaveMedicalRecord", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear registro médico");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, EditMedicalRecordsModel model)
        {
            try
            {
                var dto = MedicalRecordsFactory.CreateUpdateDto(model);

                var result = await _apiClient.PutAsync<UpdateMedicalRecordsDto, OperationResult>(
                    $"MedicalRecords/UpdateMedicalRecord/{id}", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar registro médico con ID {id}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var dto = new RemoveMedicalRecordsDto { MedicalRecordsId = id };
                var result = await _apiClient.DeleteAsync<RemoveMedicalRecordsDto, OperationResult>(
                    "MedicalRecords/DeleteMedicalRecord", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar registro médico con ID {id}");
                throw;
            }
        }
    }
}