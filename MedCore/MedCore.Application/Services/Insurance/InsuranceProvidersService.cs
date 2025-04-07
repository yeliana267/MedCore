

using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
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
            OperationResult operationResult = new OperationResult();

            try
            {
               var insuranceProviders = (await _insuranceProvidersRepository.GetAllAsync())
                    .Select(ip => new Dtos.Insurance.InsuranceProviders.InsuranceProvidersDto() 
                    {
                        InsuranceProviderID = ip.Id,
                        Name = ip.Name,
                        ContactNumber = ip.ContactNumber,
                        Email = ip.Email,
                        Website = ip.Website,
                        Address = ip.Address,
                        City = ip.City,
                        State = ip.State,
                        Country = ip.Country,
                        ZipCode = ip.ZipCode,
                        CoverageDetails = ip.CoverageDetails,
                        LogoUrl = ip.LogoUrl,
                        IsPreferred = ip.IsPreferred,
                        NetworkTypeId = ip.NetworkTypeId,
                        CustomerSupportContact = ip.CustomerSupportContact,
                        AcceptedRegions = ip.AcceptedRegions,
                        MaxCoverageAmount = ip.MaxCoverageAmount
                    }).ToList();

                // Validar si se obtuvieron datos
                if (!insuranceProviders.Any())
                {
                    operationResult.Success = false;
                    operationResult.Message = _configuration["No Insurance Providers Found"]!;
                    _logger.LogWarning(operationResult.Message);
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

        public async Task<OperationResult> GetById(int Id)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
               var insuranceProviders = await _insuranceProvidersRepository.GetEntityByIdAsync(Id);

                if (Id <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = _configuration["Error Insurance Providers"]!;
                }
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

        public async Task<OperationResult> Remove(RemoveInsuranceProvidersDto dto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {

                operationResult = await _insuranceProvidersRepository.DeleteEntityByIdAsync(dto.InsuranceProviderID);
                operationResult.Success = true;
                operationResult.Message = "Borrado exitosamente";

                if(dto.InsuranceProviderID <= 0)
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
                if(dto == null)
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
                if (dto.NetworkTypeId < 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Ingrese un NetworkTypeId valido";
                    _logger.LogWarning("Intento de Guardar fallido: NetworkTypeId no valido");
                    return operationResult;
                }

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

                insuranceProvider.Name = dto.Name;
                insuranceProvider.ContactNumber = dto.ContactNumber;
                insuranceProvider.Email = dto.Email;
                insuranceProvider.Website = dto.Website;
                insuranceProvider.Address = dto.Address;
                insuranceProvider.City = dto.City;
                insuranceProvider.State = dto.State;
                insuranceProvider.Country = dto.Country;
                insuranceProvider.ZipCode = dto.ZipCode;
                insuranceProvider.CoverageDetails = dto.CoverageDetails;
                insuranceProvider.LogoUrl = dto.LogoUrl;
                insuranceProvider.IsPreferred = dto.IsPreferred;
                insuranceProvider.NetworkTypeId = dto.NetworkTypeId;
                insuranceProvider.CustomerSupportContact = dto.CustomerSupportContact;
                insuranceProvider.AcceptedRegions = dto.AcceptedRegions;
                insuranceProvider.MaxCoverageAmount = dto.MaxCoverageAmount;
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
