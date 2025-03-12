

using MedCore.Application.Dtos.Insurance.InsuranceProvider;
using MedCore.Application.Dtos.Insurance.InsuranceProviders;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Domain.Base;
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
                operationResult.Message = "";
                _logger.LogError("", ex.ToString());
            }
            return operationResult;
       
        }

        public async Task<OperationResult> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Remove(RemoveInsuranceProvidersDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Save(SaveInsuranceProvidersDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Update(UpdateInsuranceProvidersDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
