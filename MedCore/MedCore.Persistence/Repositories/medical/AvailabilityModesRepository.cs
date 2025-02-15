using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.EntityFrameworkCore;

namespace MedCore.Persistence.Repositories.medical
{
    public class AvailabilityModesRepository : BaseRepository<AvailabilityModes, short>, IAvailabilityModesRepository
    {
        private readonly MedCoreContext Context;
        public AvailabilityModesRepository(MedCoreContext context) : base(context)
        {
            this.Context = context;
        }

        private bool ValidateAvailabilityModeNotEmpty(AvailabilityModes entity)
        {
            return !string.IsNullOrWhiteSpace(entity.AvailabilityMode);
        }

        private async Task<bool> ValidateAvailabilityModeUniqueAsync(AvailabilityModes entity)
        {
            return !await Context.AvailabilityModes.AnyAsync(e => e.AvailabilityMode == entity.AvailabilityMode && e.Id != entity.Id);
        }

        public override async Task<OperationResult> SaveEntityAsync(AvailabilityModes entity)
        {
            OperationResult result = new OperationResult();

            if (!ValidateAvailabilityModeNotEmpty(entity))
            {
                result.Success = false;
                result.Message = "El modo de disponibilidad no puede estar vacío.";
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

        public override async Task<OperationResult> UpdateEntityAsync(AvailabilityModes entity)
        {
            OperationResult result = new OperationResult();

            if (!ValidateAvailabilityModeNotEmpty(entity))
            {
                result.Success = false;
                result.Message = "El modo de disponibilidad no puede estar vacío.";
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
    }
}
