using System.Text.RegularExpressions;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.medical
{
    public class AvailabilityModesRepository : BaseRepository<AvailabilityModes, short>, IAvailabilityModesRepository
    {
        private readonly MedCoreContext Context;
        private readonly ILogger<AvailabilityModesRepository> Logger;
        private readonly IConfiguration configuration;
        public AvailabilityModesRepository(MedCoreContext context, 
                                                          ILogger<AvailabilityModesRepository> logger, 
                                                          IConfiguration configuracion) : base(context)
        {
            this.Context = context;
            this.Logger = logger;
            this.configuration = configuracion;
        }

        private bool ValidateAvailabilityModeNotEmpty(AvailabilityModes entity)
        {
            return !string.IsNullOrWhiteSpace(entity.AvailabilityMode);
        }

        private bool ValidateAvailabilityModeNoNumbers(AvailabilityModes entity)
        {
            return !Regex.IsMatch(entity.AvailabilityMode, @"\d");
        }

        private bool ValidateAvailabilityModeMinLength(AvailabilityModes entity)
        {
            return entity.AvailabilityMode.Length >= 5;
        }

        private async Task<bool> ValidateAvailabilityModeUniqueAsync(AvailabilityModes entity)
        {
            try
            {
                return !await Context.AvailabilityModes.AnyAsync(e => e.AvailabilityMode == entity.AvailabilityMode && e.Id != entity.Id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error validating uniqueness of AvailabilityMode.");
                throw;
            }
        }

        public override async Task<OperationResult> SaveEntityAsync(AvailabilityModes entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (!ValidateAvailabilityModeNotEmpty(entity))
                {
                    result.Success = false;
                    result.Message = "El modo de disponibilidad no puede estar vacío.";
                    return result;
                }

                if (!ValidateAvailabilityModeNoNumbers(entity))
                {
                    result.Success = false;
                    result.Message = "El modo de disponibilidad no puede contener números.";
                    return result;
                }

                if (!ValidateAvailabilityModeMinLength(entity))
                {
                    result.Success = false;
                    result.Message = "El modo de disponibilidad debe tener al menos 5 caracteres.";
                    return result;
                }

                if (!await ValidateAvailabilityModeUniqueAsync(entity))
                {
                    result.Success = false;
                    result.Message = "El modo de disponibilidad ya existe.";
                    return result;
                }

                return await base.SaveEntityAsync(entity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error saving AvailabilityMode.");
                result.Success = false;
                result.Message = $"Ocurrió un error guardando el modo de disponibilidad: {ex.Message}";
                return result;
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(AvailabilityModes entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (!ValidateAvailabilityModeNotEmpty(entity))
                {
                    result.Success = false;
                    result.Message = "El modo de disponibilidad no puede estar vacío.";
                    return result;
                }

                if (!ValidateAvailabilityModeNoNumbers(entity))
                {
                    result.Success = false;
                    result.Message = "El modo de disponibilidad no puede contener números.";
                    return result;
                }

                if (!ValidateAvailabilityModeMinLength(entity))
                {
                    result.Success = false;
                    result.Message = "El modo de disponibilidad debe tener al menos 5 caracteres.";
                    return result;
                }

                if (!await ValidateAvailabilityModeUniqueAsync(entity))
                {
                    result.Success = false;
                    result.Message = "El modo de disponibilidad ya existe.";
                    return result;
                }

                return await base.UpdateEntityAsync(entity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating AvailabilityMode.");
                result.Success = false;
                result.Message = $"Ocurrió un error actualizando el modo de disponibilidad: {ex.Message}";
                return result;
            }
        }
    }
}
