using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Application.Dtos.Insurance.InsuranceProvider;
using MedCore.Application.Dtos.Insurance.InsuranceProviders;
using MedCore.Application.Dtos.Insurance.NetworkType;
using MedCore.Domain.Base;
using MedCore.Web.Interfaces;
using MedCore.Web.Interfaces.Insurance;
using MedCore.Web.Models.Insurance.InsuranceProviders;
using MedCore.Web.Models.Insurance.NetworkType;
using MedCore.Web.Repositories.appointments;
using MedCore.Web.Repositories.appointmentWeb;
using MedCore.Web.Repositories.InsuranceWeb.InsurancePrividersWeb;
using MedCore.Web.Repositories.InsuranceWeb.NetworkTypeWeb;

namespace MedCore.Web.Repositories.InsuranceWeb.InsuranceProvidersWeb
{
    public class InsuranceProvidersWeb: IInsuranceProvidersWeb
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<InsuranceProvidersWeb> _logger;

        public InsuranceProvidersWeb(IApiClient apiClient, ILogger<InsuranceProvidersWeb> logger)
        {
            _apiClient = apiClient;
            _logger = logger;

        }
        public async Task<bool> CreateAsync(CreateInsuranceProvidersModel model)
        {
            try
            {
                var dto = InsuranceProvidersFactory.CreateSaveDto(model);

                var result = await _apiClient.PostAsync<SaveInsuranceProvidersDto, OperationResult>(
                    "InsuranceProviders/SaveInsuranceProviders", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear Proveedor de seguro");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var dto = new RemoveInsuranceProvidersDto { InsuranceProviderID = id };
                var result = await _apiClient.DeleteAsync<RemoveInsuranceProvidersDto, OperationResult>(
                    "InsuranceProviders/DeleteInsuranceProviders", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar Proveedor de Seguro con ID {id}");
                throw;
            }
        }

        public async Task<List<InsuranceProvidersModel>> GetAllAsync()
        {
            try
            {
                var insurancesProviders = await _apiClient.GetAsync<List<InsuranceProvidersModel>>("InsuranceProviders/GetInsuranceProviders");
                return insurancesProviders ?? new List<InsuranceProvidersModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los proveedores");
                throw;
            }
        }

        public async Task<InsuranceProvidersModel> GetByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetAsync<InsuranceProvidersModel>($"InsuranceProviders/GetInsuranceProvidersById?Id={id}")
                    ?? new InsuranceProvidersModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el proveedor con ID {id}");
                throw;
            }
        }

        public async Task<EditInsuranceProvidersModel> GetEditModelByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetAsync<EditInsuranceProvidersModel>($"InsuranceProviders/UpdateInsuranceProviders?Id={id}")
                    ?? new EditInsuranceProvidersModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener modelo de edición para Proveedor de seguro con ID {id}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, EditInsuranceProvidersModel model)
        {
            try
            {
                var dto = InsuranceProvidersFactory.CreateUpdateDto(model);

                var result = await _apiClient.PutAsync<UpdateInsuranceProvidersDto, OperationResult>(
                    $"InsuranceProviders/UpdateInsuranceProviders/{id}", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar cita con ID {id}");
                throw;
            }
        }
    }
}
