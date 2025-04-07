

using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Application.Dtos.Insurance.NetworkType;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Interfaces.Insurance;
using MedCore.Persistence.Repositories.Insurance;
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
            var result = new OperationResult();
            try
            {
                var data = await _networkTypeRepository.GetAllAsync();
                result.Data = data;
                result.Success = true;

                // Validar si se obtuvieron datos
                if (!data.Any())
                {
                    result.Success = false;
                    result.Message = _configuration["Error NetworkType"]!;
                    _logger.LogWarning(result.Message);
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error NetworkType: {ex.Message}";
                _logger.LogError(result.Message, ex);
            }
            return result;
        }

        public async Task<OperationResult> GetById(int Id)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                
                var networkType = await _networkTypeRepository.GetEntityByIdAsync(Id);
                operationResult.Success = true;
                operationResult.Data = networkType;
                if (networkType == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Network Types not found.";
                    return operationResult;
                }
                if (Id <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Network Types not found.";
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

        public async Task<OperationResult> Save(SaveNetworkTypeDto dto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                if (dto == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "La entidad no puede ser nula.";
                    return operationResult;
                }
                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    operationResult.Success = false;
                    operationResult.Message = "Name no puede estar vacio.";
                    return operationResult;
                }
                if (dto.Name.Length > 50)
                {
                    operationResult.Success = false;
                    operationResult.Message = "La longitud del nombre no puede exceder los 50 caracteres.";
                    return operationResult;
                }
                if (dto.Description?.Length > 255)
                {
                    operationResult.Success = false;
                    operationResult.Message = "La longitud de la descripcion no puede exceder los 255 caracteres.";
                    return operationResult;
                }

                var network = await _networkTypeRepository.SaveEntityAsync(new NetworkType()
                { Name = dto.Name,
                  Description = dto.Description
                });
                operationResult.Success = true;
                operationResult.Message = "Guardado Exitosamente";
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
                    return operationResult;
                }


                networktype.Name = dto.Name;
                networktype.Description = dto.Description;
                networktype.UpdatedAt = DateTime.Now;
                _logger.LogInformation($"Actualizando NetworkType {dto.NetworkTypeId}");
                
                await _networkTypeRepository.UpdateEntityAsync(dto.NetworkTypeId, networktype);
                operationResult.Success = true;
                operationResult.Message = "Actualizacion correcta";

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = _configuration["Network type not Update"]!;
                _logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }

        public async Task<OperationResult> Remove(RemoveNetwokTypeDto networkTypeDto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                if (networkTypeDto.NetworkTypeId <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Tipo de Red de Seguro no encontrado";
                    return operationResult;
                }
                if (networkTypeDto == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Tipo de Red de Seguro no encontrado";
                    return operationResult;
                   
                }

                operationResult = await _networkTypeRepository.DeleteEntityByIdAsync(networkTypeDto.NetworkTypeId);
                operationResult.Success = true;
                operationResult.Message = "Tipo de Red de Seguro borrada exitosamente";

              

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = _configuration["Error NetworkType"]!;
                _logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }
    }
}
