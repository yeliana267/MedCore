using MedCore.Domain.Base;
using MedCore.Domain.Entities;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.medical;

namespace MedCore.Persistence.Repositories.medical
{
    public class SpecialtiesRepository : BaseRepository<Specialties, short>, ISpecialtiesRepository
    {
        readonly MedCoreContext Context;
        public SpecialtiesRepository(MedCoreContext context) : base(context)
        {
            this.Context = context;
        } 

        private bool ValidateSpecialtyNotEmpty(Specialties entity)
        {
            return !string.IsNullOrWhiteSpace(entity.SpecialtyName);
        }

        private bool ValidateSpecialtyUnique(Specialties entity)
        {
            return !Context.Specialties.Any(e => e.SpecialtyName == entity.SpecialtyName && e.Id != entity.Id);
        }

        public override async Task<OperationResult> SaveEntityAsync(Specialties entity)
        {
            OperationResult result = new OperationResult();
            if (!ValidateSpecialtyNotEmpty(entity))
            {
                result.Success = false;
                result.Message = "El nombre de la especialidad no puede estar vacío.";
                return result;
            }
            if (!ValidateSpecialtyUnique(entity))
            {
                result.Success = false;
                result.Message = "La especialidad ya existe.";
                return result;
            }
            return await base.SaveEntityAsync(entity);
        }

        public override Task<OperationResult> UpdateEntityAsync(Specialties entity)
        {
            OperationResult result = new OperationResult();
            if (!ValidateSpecialtyNotEmpty(entity))
            {
                result.Success = false;
                result.Message = "El nombre de la especialidad no puede estar vacío.";
                return Task.FromResult(result);
            }
            if (!ValidateSpecialtyUnique(entity))
            {
                result.Success = false;
                result.Message = "La especialidad ya existe.";
                return Task.FromResult(result);
            }
            return base.UpdateEntityAsync(entity);
        }
    }
}
