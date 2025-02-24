

using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Model.Models.Insurance;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.Insurance
{
    public class NetworkTypeRepository : BaseRepository<NetworkType, int>, INetworkTypeRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<NetworkTypeRepository> _logger;
        private readonly IConfiguration _configuration;
        public NetworkTypeRepository(MedCoreContext context,
                                     ILogger<NetworkTypeRepository> logger,
                                     IConfiguration configuration) : base(context)
        {
            this._context = context;
            this._logger = logger;
            this._configuration = configuration;
        }

        public async Task<OperationResult> GetNetworkTypeById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var networkType = await _context.NetworkType.FindAsync(id);

                if (networkType == null)
                {
                    result.Success = false;
                    result.Message = "Network type not found.";
                    return result;
                }

                result.Success = true;
                result.Data = networkType;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error retrieving network type with ID {Id}", id);
                result.Success = false;
                result.Message = this._configuration["An error occurred while fetching the network type."]!;
            }

            return result;

        }

        public async Task<OperationResult> GetNetworkTypeList()
        {
            OperationResult result = new OperationResult();

            try
            {

                var query = await (from networkType in _context.NetworkType
                                   select new NetworkTypeModel()
                                   { Name = networkType.Name }).ToListAsync();

                if (query.Count == 0)
                {
                    result.Message = "No NetworkType found list";
                }

                result.Data = query;

            }
            catch (Exception ex) 
            {

                result.Message = this._configuration["ErrorNetworkTypeRepository:GetNetworkType"]!;
                this._logger.LogError(result.Message,ex.ToString());
            }

            return result;
        }
    }
}
