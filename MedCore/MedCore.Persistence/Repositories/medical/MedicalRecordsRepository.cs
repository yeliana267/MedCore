using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.medical;

namespace MedCore.Persistence.Repositories.medical
{
    public class MedicalRecordsRepository : BaseRepository<MedicalRecords, int>, IMedicalRecordsRepository
    {
        readonly MedCoreContext Context;
        public MedicalRecordsRepository(MedCoreContext context) : base(context)
        {
            this.Context = context;
        }
        private bool ValidateDiagnosisNotEmpty(MedicalRecords entity)
        {
            return !string.IsNullOrWhiteSpace(entity.Diagnosis);
        }

        private bool ValidateDateOfVisitNotInFuture(MedicalRecords entity)
        {
            return entity.DateOfVisit <= DateTime.Now;
        }

        public override async Task<OperationResult> SaveEntityAsync(MedicalRecords entity)
        {
            OperationResult result = new OperationResult();
            if (!ValidateDiagnosisNotEmpty(entity))
            {
                result.Success = false;
                result.Message = "El diagnóstico no puede estar vacío.";
                return result;
            }
            if (!ValidateDateOfVisitNotInFuture(entity))
            {
                result.Success = false;
                result.Message = "La fecha de visita no puede estar en el futuro.";
                return result;
            }
            return await base.SaveEntityAsync(entity);
        }
        
        public override async Task<OperationResult> UpdateEntityAsync(MedicalRecords entity)
        {
            OperationResult result = new OperationResult();
            if (!ValidateDiagnosisNotEmpty(entity))
            {
                result.Success = false;
                result.Message = "El diagnóstico no puede estar vacío.";
                return result;
            }
            if (!ValidateDateOfVisitNotInFuture(entity))
            {
                result.Success = false;
                result.Message = "La fecha de visita no puede estar en el futuro.";
                return result;
            }
            return await base.UpdateEntityAsync(entity);
        }
    }
}

