

using System.Text.RegularExpressions;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
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
                _logger.LogError(ex, "Error retrieving Insurance Providers with ID {Id}", id);
                result.Success = false;
                result.Message = _configuration["An error occurred while fetching the Insurance Providers."]!;
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
                    return result;
                }
                if (string.IsNullOrWhiteSpace(entity!.Name) || entity.Name.Length > 100)
                {
                    result.Success = false;
                    result.Message = "Name no puede estar vacio. La longitud del nombre no puede exceder los 100 caracteres";
                    return result;
                }
                if(_context.InsuranceProviders.Any(de => de.Name == entity.Name))
                {
                    result.Success = false;
                    result.Message = "El Insurance Providers no puede estar duplicado";
                    return result;
                }
                if (string.IsNullOrEmpty(entity.ContactNumber))
                {
                    result.Success = false;
                    result.Message = "ContactNumber no puede estar vacio.";
                    return result;
                }
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (string.IsNullOrEmpty(entity.Email) || !Regex.IsMatch(entity.Email, emailPattern))
                {
                    result.Success = false;
                    result.Message = "El formato del correo electrónico no es válido.";
                    _logger.LogWarning("Intento de actualización fallido: Formato de correo electrónico no válido.");
                    return result;
                }
                if (string.IsNullOrEmpty(entity.Address) || entity.Address.Length > 255)
                {
                    result.Success = false;
                    result.Message = "El formato del Address electrónico no es válido.";
                    _logger.LogWarning("Intento de Guardar fallido: Formato de Address electrónico no válido.");
                    return result;
                }
                if (entity.NetworkTypeId < 0)
                {
                    result.Success = false;
                    result.Message = "Ingrese un NetworkTypeId valido";
                    _logger.LogWarning("Intento de Guardar fallido: NetworkTypeId no valido");
                    return result;
                }
                _context.InsuranceProviders.Add(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Entidad guardada exitosamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} guardando la entidad.";
                _logger.LogError(ex, "Error en guardar Insurance Providers");
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
                if (entity.NetworkTypeId < 0)
                {
                    result.Success = false;
                    result.Message = "Ingrese un NetworkTypeId valido";
                    _logger.LogWarning("Intento de actualizacion fallido: NetworkTypeId no valido");
                    return result;
                }

                _logger.LogInformation($"Actualizando InsuranceProviders {entity.Id}");

                insuranceProviders.Name = entity.Name;
                insuranceProviders.ContactNumber = entity.ContactNumber;
                insuranceProviders.Email = entity.Email;
                insuranceProviders.Website = entity.Website;
                insuranceProviders.Address = entity.Address;
                insuranceProviders.City = entity.City;
                insuranceProviders.Country = entity.Country;
                insuranceProviders.ZipCode = entity.ZipCode;
                insuranceProviders.CoverageDetails = entity.CoverageDetails;
                insuranceProviders.LogoUrl = entity.LogoUrl;
                insuranceProviders.IsPreferred = entity.IsPreferred;
                insuranceProviders.NetworkTypeId = entity.NetworkTypeId;
                insuranceProviders.AcceptedRegions = entity.AcceptedRegions;
                insuranceProviders.MaxCoverageAmount = entity.MaxCoverageAmount;
                insuranceProviders.IsActive = entity.IsActive;
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

