

using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection.Emit;
using MedCore.Application.Dtos.Insurance.InsuranceProvider;
using MedCore.Application.Dtos.Insurance.InsuranceProviders;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Domain.Base;
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
               var insuranceProviders = await _insuranceProvidersRepository.GetAllAsync();
            }
            catch(Exception ex)
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
                if (Id > 0)
                {
                    var insuranceProviders = await _insuranceProvidersRepository.GetEntityByIdAsync(Id);
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

        public async Task<OperationResult> Remove(RemoveInsuranceProvidersDto dto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                operationResult = await _insuranceProvidersRepository.DeleteEntityByIdAsync(dto.InsuranceProviderID);
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
                var insuranceProvider = await _insuranceProvidersRepository.GetEntityByIdAsync(dto.InsuranceProviderID);

                if (insuranceProvider != null) {

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
                else
                {
                    operationResult.Message = "Insurance Providers not Update";
                    operationResult.Success = false;
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
    }
}
