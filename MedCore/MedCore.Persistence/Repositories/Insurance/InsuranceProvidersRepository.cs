

using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Model.Models.Insurance;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MedCore.Persistence.Repositories.Insurance
{
    public class InsuranceProvidersRepository : BaseRepository<InsuranceProviders, int> , IInsuranceProvidersRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<InsuranceProvidersRepository> _logger;
        private readonly IConfiguration _configuration;
        public InsuranceProvidersRepository(MedCoreContext context, 
                                            ILogger<InsuranceProvidersRepository> logger, 
                                            IConfiguration configuration) : base(context) 
        { 
            
           this._context = context;
           this._logger = logger;
           this._configuration = configuration;
        }

        public async Task<OperationResult> CountInsuranceProvidersByNetworkTypeId(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                int count = await _context.InsuranceProviders
                    .Where(ip => ip.NetworkTypeId == id)
                    .CountAsync();

                result.Data = count;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = this._configuration["ErrorInsuranceProvidersRepository:CountInsuranceProvidersByNetworkTypeId"]!;
                this._logger.LogError(result.Message, ex);
            }

            return result;
        }

    }
}
