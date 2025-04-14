

using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using MedCore.Application.Dtos.Insurance.InsuranceProvider;
using MedCore.Application.Dtos.Insurance.InsuranceProviders;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.Insurance;
using MedCore.Persistence.Interfaces.Insurance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.Insurance
{
    public class InsuranceProvidersService : IInsuranceProvidersService
    {
        private readonly IInsuranceProvidersRepository _insuranceProvidersRepository;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public InsuranceProvidersService(IInsuranceProvidersRepository insuranceProvidersRepository,
                                         ILogger<InsuranceProvidersService> logger,
                                         IConfiguration configuration)
        {
            _insuranceProvidersRepository = insuranceProvidersRepository;
            _logger = logger; 
            _configuration = configuration;
        }
       
        public async Task<OperationResult> GetAll()
        {
            var result = new OperationResult();
            try
            {
                var data = await _insuranceProvidersRepository.GetAllAsync();
                result.Data = data;
                result.Success = true;

                // Validar si se obtuvieron datos
                if (!data.Any())
                {
                    result.Success = false;
                    result.Message = _configuration["No Insurance Providers Found"]!;
                    _logger.LogWarning(result.Message);
                }            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error Insurance Providers: {ex.Message}";
                _logger.LogError(result.Message, ex);
            }
            return result;
        }

        public async Task<OperationResult> GetById(int Id)
        {
            OperationResult operationResult = new OperationResult();
            try
            {

                if (Id <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = _configuration["Error Insurance Providers"]!;
                }

                var insuranceProviders = await _insuranceProvidersRepository.GetEntityByIdAsync(Id);
                if (insuranceProviders == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Insurance Providers not found.";
                    return operationResult;
                }

                operationResult.Success = true;
                operationResult.Data = insuranceProviders;

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = _configuration["Error Insurance Providers"]!;
                _logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }


        public async Task<OperationResult> Remove(RemoveInsuranceProvidersDto insuranceProvidersDto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {

                operationResult = await _insuranceProvidersRepository.DeleteEntityByIdAsync(insuranceProvidersDto.InsuranceProviderID);
                operationResult.Success = true;
                operationResult.Message = "Borrado exitosamente";

                if(insuranceProvidersDto.InsuranceProviderID <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Error borrando";
                }

            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = _configuration["Error Insurance Providers"]!;
                _logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }

        public async Task<OperationResult> Save(SaveInsuranceProvidersDto dto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
               

                var insuranceProvider = await _insuranceProvidersRepository.SaveEntityAsync(new InsuranceProviders()
                {
                    Name = dto.Name,
                    ContactNumber = dto.ContactNumber,
                    Email = dto.Email,
                    Website = dto.Website,
                    Address = dto.Address,
                    City = dto.City,
                    State = dto.State,
                    Country = dto.Country,
                    ZipCode = dto.ZipCode,
                    CoverageDetails = dto.CoverageDetails,
                    LogoUrl = dto.LogoUrl,
                    IsPreferred = dto.IsPreferred,
                    NetworkTypeId = dto.NetworkTypeId,
                    CustomerSupportContact = dto.CustomerSupportContact,
                    AcceptedRegions = dto.AcceptedRegions,
                    MaxCoverageAmount = dto.MaxCoverageAmount,
                });
                operationResult.Success = true;
                operationResult.Message = "Guardado exitosamente";                
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = _configuration["Error Insurance Providers"]!;
                _logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }
        public async Task<OperationResult> Update(UpdateInsuranceProvidersDto dto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
               
                if(dto.InsuranceProviderID <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Invalid Insurance Provider ID.";
                    return operationResult;
                }
                if (dto == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "No puede ser null";
                    return operationResult;
                }
                if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length > 100)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Name is required.";
                    return operationResult;
                }
                if (string.IsNullOrEmpty(dto.ContactNumber))
                {
                    operationResult.Success = false;
                    operationResult.Message = "ContactNumber no puede estar vacio.";
                    return operationResult;
                }
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (string.IsNullOrEmpty(dto.Email) || !Regex.IsMatch(dto.Email, emailPattern))
                {
                    operationResult.Success = false;
                    operationResult.Message = "El formato del correo electrónico no es válido.";
                    _logger.LogWarning("Intento de actualización fallido: Formato de correo electrónico no válido.");
                    return operationResult;
                }
                if (string.IsNullOrEmpty(dto.Address) || dto.Address.Length > 255)
                {
                    operationResult.Success = false;
                    operationResult.Message = "El formato del Address electrónico no es válido.";
                    _logger.LogWarning("Intento de Guardar fallido: Formato de Address electrónico no válido.");
                    return operationResult;
                }
                if (dto.NetworkTypeId <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Ingrese un NetworkTypeId valido";
                    _logger.LogWarning("Intento de Guardar fallido: NetworkTypeId no valido");
                    return operationResult;
                }

                var insuranceProvider = await _insuranceProvidersRepository.GetEntityByIdAsync(dto.InsuranceProviderID);

                if (insuranceProvider == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = $"Insurance Provider with ID {dto.InsuranceProviderID} not found.";
                    return operationResult;
                }


                await _insuranceProvidersRepository.UpdateEntityAsync(dto.InsuranceProviderID, insuranceProvider);

                operationResult.Success = true;
                operationResult.Message = "Insurance Providers Update";


            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = _configuration["Error Insurance Providers"]!;
                _logger.LogError(operationResult.Message, ex);
            }
            return operationResult;
        }
    }
}
