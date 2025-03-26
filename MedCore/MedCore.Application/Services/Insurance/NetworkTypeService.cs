

using MedCore.Application.Dtos.Insurance.NetworkType;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Persistence.Interfaces.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                operationResult.Success = false;
                operationResult.Message = _configuration["Error NetworkType"]!;
                _logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }

        public async Task<OperationResult> GetById(int Id)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                
                var networkType = await _networkTypeRepository.GetEntityByIdAsync(Id);
                operationResult.Success = true;

                if (networkType == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Insurance Providers not found.";
                    return operationResult;
                }
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = _configuration["Error NetworkType"]!;
                _logger.LogError(operationResult.Message, ex);
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
                operationResult.Success = false;
                operationResult.Message = _configuration["Error NetworkType"]!;
                _logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }

        public async Task<OperationResult> Save(SaveNetworkTypeDto dto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {

                var network = await _networkTypeRepository.SaveEntityAsync(new NetworkType()
                { Name = dto.Name,
                  Description = dto.Description
                });
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = _configuration["NetworkType not save"]!;
                _logger.LogError(operationResult.Message, ex);

            }
            return operationResult;
        }

        public async Task<OperationResult> Update(UpdateNetworkTypeDto dto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var networktype = await _networkTypeRepository.GetEntityByIdAsync(dto.NetworkTypeId);

                
                if (networktype == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = $"No se encontró NetworkType por ID {dto.NetworkTypeId}.";
                    _logger.LogWarning($"Intento de actualización fallido: NetworkType {dto.NetworkTypeId} no encontrada.");
                    return operationResult;
                }

                if (networktype.Name == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "El nombre no puede ser null";
                    _logger.LogWarning($"Intento de actualización fallido: Coloque el Nombre");
                }


                networktype.Name = dto.Name;
                networktype.Description = dto.Description;
                networktype.UpdatedAt = DateTime.Now;
                _logger.LogInformation($"Actualizando NetworkType {dto.NetworkTypeId}");
                await _networkTypeRepository.UpdateEntityAsync(dto.NetworkTypeId, networktype);
                
                
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = _configuration["Network type not Update"]!;
                _logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }
    }
}
