

using MedCore.Application.Dtos.Insurance.NetworkType;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Domain.Base;
using MedCore.Persistence.Interfaces.Insurance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.Insurance
{
    public class NetworkTypeService : INetworkTypeService
    {
        private readonly INetworkTypeRepository _networkTypeRepository;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public NetworkTypeService(INetworkTypeRepository networkTypeRepository, 
                                  ILogger<NetworkTypeService>logger, 
                                  IConfiguration configuration) 
        { 
                                  
         _networkTypeRepository = networkTypeRepository;
         _logger = logger; _configuration = configuration;
        }
        public async Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var networkTypes = await _networkTypeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                operationResult.Message = "";
                _logger.LogError("", ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult> GetById(int Id)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var networkType = await _networkTypeRepository.GetEntityByIdAsync(Id);
            }
            catch (Exception ex)
            {
                operationResult.Message = "";
                _logger.LogError("", ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult> Remove(RemoveNetwokTypeDto dto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                if(dto.NetworkTypeId > 0)
                {
                    operationResult = await _networkTypeRepository.DeleteEntityByIdAsync(dto.NetworkTypeId);
                }
            }
            catch (Exception ex)
            {
                operationResult.Message = "";
                _logger.LogError("", ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult> Save(SaveNetworkTypeDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Update(UpdateNetworkTypeDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
