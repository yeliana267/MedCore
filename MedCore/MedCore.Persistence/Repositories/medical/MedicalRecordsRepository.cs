using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.medical
{
    public class MedicalRecordsRepository : BaseRepository<MedicalRecords, int>, IMedicalRecordsRepository
    {
        private readonly MedCoreContext Context;
        private readonly ILogger<MedicalRecordsRepository> logger;
        private readonly IConfiguration configuracion;

        public MedicalRecordsRepository(MedCoreContext context, 
                                                       ILogger<MedicalRecordsRepository> logger, 
                                                       IConfiguration configuracion) : base(context)
        {
            this.Context = context;
            this.logger = logger;
            this.configuracion = configuracion;
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

            try
            {
                if (!ValidateDiagnosisNotEmpty(entity))
                {
                    result.Success = false;
                    result.Message = "El diagnóstico no puede estar vacío.";
                    return result;
                }

                if (!ValidateDateOfVisitNotInFuture(entity))
                {
                    result.Success = false;
                    result.Message = "La fecha de la visita no puede ser una fecha futura.";
                    return result;
                }

                return await base.SaveEntityAsync(entity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error saving MedicalRecord.");
                result.Success = false;
                result.Message = $"Ocurrió un error guardando el registro médico: {ex.Message}";
                return result;
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(MedicalRecords entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (!ValidateDiagnosisNotEmpty(entity))
                {
                    result.Success = false;
                    result.Message = "El diagnóstico no puede estar vacío.";
                    return result;
                }

                if (!ValidateDateOfVisitNotInFuture(entity))
                {
                    result.Success = false;
                    result.Message = "La fecha de la visita no puede ser una fecha futura.";
                    return result;
                }

                return await base.UpdateEntityAsync(entity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating MedicalRecord.");
                result.Success = false;
                result.Message = $"Ocurrió un error actualizando el registro médico: {ex.Message}";
                return result;
            }
        }
    }
}