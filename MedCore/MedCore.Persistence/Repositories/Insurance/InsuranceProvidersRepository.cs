

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
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
                result.Message = _configuration["ErrorInsuranceProvidersRepository:CountInsuranceProvidersByNetworkTypeId"]!;
                _logger.LogError(result.Message, ex);
            }

            return result;
        }
        
        public async Task<OperationResult> GetInsuranceProvidersById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var insuranceProviders = await _context.NetworkType.FindAsync(id);

                if (insuranceProviders == null)
                {
                    result.Success = false;
                    result.Message = "Insurance Providers not found.";
                    return result;
                }

                result.Success = true;
                result.Data = insuranceProviders;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error retrieving Insurance Providers with ID {Id}", id);
                result.Success = false;
                result.Message = this._configuration["An error occurred while fetching the Insurance Providers."]!;
            }

            return result;

        }

        public override async Task<OperationResult> SaveEntityAsync(InsuranceProviders entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "La entidad no puede ser nula";
                }
                else
                {
                    _context.InsuranceProviders.Add(entity);
                    await _context.SaveChangesAsync();
                    result.Success = true;
                    result.Message = "Entidad guardada exitosamente.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} guardando la entidad.";
            }
            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(int id, InsuranceProviders entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                var insuranceProviders = await _context.InsuranceProviders.FindAsync(id);

                if (insuranceProviders == null)
                {
                    result.Success = false;
                    result.Message = $"No se encontró InsuranceProviders por ID {id}.";
                    _logger.LogWarning($"Intento de actualización fallido: InsuranceProviders {id} no encontrada.");
                    return result;
                }

                _logger.LogInformation($"Actualizando InsuranceProviders {entity.Id}");

                insuranceProviders.Name = entity.Name;
                insuranceProviders.UpdatedAt = DateTime.Now;

                _context.InsuranceProviders.Update(insuranceProviders);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = $"InsuranceProviders con ID {id} actualizada correctamente.";
                _logger.LogInformation($"InsuranceProviders {id} actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar InsuranceProviders: {ex.Message}";
                _logger.LogError($"Error en UpdateEntityAsync para InsuranceProviders {id}: {ex.Message}", ex);
            }

            return result;
        }


        public override async Task<OperationResult> DeleteEntityByIdAsync(int InsuranceProviderID)
        {
            OperationResult result = new OperationResult();

            try
            {
                var querys = await _context.Database.ExecuteSqlRawAsync("DELETE FROM [MedicalAppointment].[Insurance].[InsuranceProviders] WHERE [InsuranceProviderID] = {0}", InsuranceProviderID);

                if (querys > 0)
                {
                    result.Success = true;
                    result.Message = "Proveedor eliminado exitosamente.";
                }
                else
                {
                    result.Success = false;
                    result.Message = "No se encontró Proveedor de Seguros con ese ID.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al eliminar un Proveedor de Seguros.";
                _logger.LogError($"Error al eliminar Proveedor de Seguros con ID {InsuranceProviderID}: {ex.Message}", ex);
            }
            return result;

        }

    }



}

