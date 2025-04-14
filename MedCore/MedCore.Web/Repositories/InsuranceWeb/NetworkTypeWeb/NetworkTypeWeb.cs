using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Application.Dtos.Insurance.NetworkType;
using MedCore.Domain.Base;
using MedCore.Web.Interfaces;
using MedCore.Web.Interfaces.Insurance;
using MedCore.Web.Models.appointments;
using MedCore.Web.Models.Insurance.NetworkType;
using MedCore.Web.Repositories.appointments;
using MedCore.Web.Repositories.appointmentWeb;
using MedCore.Web.Repositories.InsuranceWeb.InsurancePrividersWeb;

namespace MedCore.Web.Repositories.InsuranceWeb.NetworkTypeWeb
{
    public class NetworkTypeWeb: INetworkTypeWeb
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<NetworkTypeWeb> _logger;
        public NetworkTypeWeb(IApiClient apiClient, ILogger<NetworkTypeWeb> logger)
        {
            _apiClient = apiClient;
            _logger = logger; 
        }
        public async Task<bool> CreateAsync(CreateNetworkTypeModel model)
        {
            try
            {
                var dto = NetworkTypeFactory.CreateSaveDto(model);

                var result = await _apiClient.PostAsync<SaveNetworkTypeDto, OperationResult>(
                    "NetworkType/SaveNetworkType", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear tipo de red");
                throw;
            }
        }
        

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var dto = new RemoveNetwokTypeDto { NetworkTypeId = id };
                var result = await _apiClient.DeleteAsync<RemoveNetwokTypeDto, OperationResult>(
                    "NetworkType/DeleteNetworkType", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar red con ID {id}");
                throw;
            }
        }

        public async Task<List<NetworkTypeModel>> GetAllAsync()
        {
            try
            {
                var networktype = await _apiClient.GetAsync<List<NetworkTypeModel>>("NetworkType/GetNeworkType");
                return networktype ?? new List<NetworkTypeModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las redes de seguro");
                throw;
            }
        }

        public async Task<NetworkTypeModel> GetByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetAsync<NetworkTypeModel>($"NetworkType/GetNetworkTypeById?Id={id}")
                    ?? new NetworkTypeModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la red con ID {id}");
                throw;
            }
        }

        public async Task<EditNetworkTypeModel> GetEditModelByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetAsync<EditNetworkTypeModel>($"NetworkType/GetNetworkTypeById?Id={id}")
                    ?? new EditNetworkTypeModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener modelo de edición para red de seguro con ID {id}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, EditNetworkTypeModel model)
        {
            try
            {
                var dto = NetworkTypeFactory.CreateUpdateDto(model);

                var result = await _apiClient.PutAsync<UpdateNetworkTypeDto, OperationResult>(
                    $"NetworkType/UpdateNetworkType/{id}", dto);
                
                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar la red de seguro con ID {id}");
                throw;
            }
        }
    }
}
