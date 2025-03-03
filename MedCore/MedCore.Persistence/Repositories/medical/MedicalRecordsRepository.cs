using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Model.Models.medical;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.medical
{
    public class MedicalRecordsRepository : BaseRepository<MedicalRecords, int>, IMedicalRecordsRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<MedicalRecordsRepository> _logger;
        private readonly IConfiguration _configuration;

        public MedicalRecordsRepository(MedCoreContext context, ILogger<MedicalRecordsRepository> logger, IConfiguration configuration) : base(context)
        {
            this._context = context;
            this._logger = logger;
            this._configuration = configuration;
        }

        public async Task<List<OperationResult>> GetMedicalRecordsByPatientIdAsync(int patientId)
        {
            List<OperationResult> results = new List<OperationResult>();

            //Verificar que el ID del paciente sea válido
            if (patientId <= 0)
            {
                results.Add(new OperationResult
                {
                    Success = false,
                    Message = "El ID del paciente debe ser un número válido y mayor que cero."
                });
                return results;
            }

            try
            {
                var querys = await (from MedicalRecords in _context.MedicalRecords
                                    join Patient in _context.Patients on MedicalRecords.PatientID equals Patient.Id
                                    join Doctor in _context.Doctors on MedicalRecords.DoctorID equals Doctor.Id
                                    where MedicalRecords.PatientID == patientId
                                    orderby MedicalRecords.DateOfVisit descending
                                    select new MedicalRecordsModel()
                                    {
                                        Id = MedicalRecords.Id,
                                        PatientId = MedicalRecords.PatientID,
                                        DoctorId = MedicalRecords.DoctorID,
                                        Diagnosis = MedicalRecords.Diagnosis,
                                        Treatment = MedicalRecords.Treatment,
                                        DateOfVisit = MedicalRecords.DateOfVisit
                                    }).ToListAsync();

                //Verificar si no existen registros médicos para el paciente
                if (querys == null || querys.Count == 0)
                {
                    results.Add(new OperationResult
                    {
                        Success = false,
                        Message = "No se encontraron registros médicos para este paciente."
                    });
                    return results;
                }

                results.Add(new OperationResult
                {
                    Success = true,
                    Message = "Historial médico obtenido exitosamente.",
                    Data = querys
                });
            }
            catch (Exception ex)
            {
                var errorMessage = _configuration["ErrorMedicalRecordsRepository:GetMedicalRecordsByPatient"]
                                   ?? "Error desconocido al obtener el historial médico.";
                _logger.LogError("{ErrorMessage} - Exception: {Exception}", errorMessage, ex);
                results.Add(new OperationResult
                {
                    Success = false,
                    Message = errorMessage
                });
            }

            return results;
        }

        public async Task<List<OperationResult>> GetMedicalRecordsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            List<OperationResult> results = new List<OperationResult>();

            //Verificar que las fechas sean válidas
            if (startDate == default || endDate == default)
            {
                results.Add(new OperationResult
                {
                    Success = false,
                    Message = "Las fechas de inicio y fin deben ser válidas."
                });
                return results;
            }

            //Verificar que la fecha de inicio no sea mayor que la fecha de fin
            if (startDate > endDate)
            {
                results.Add(new OperationResult
                {
                    Success = false,
                    Message = "La fecha de inicio no puede ser mayor que la fecha de fin."
                });
                return results;
            }

            try
            {
                var querys = await (from MedicalRecords in _context.MedicalRecords
                                    where MedicalRecords.DateOfVisit >= startDate && MedicalRecords.DateOfVisit <= endDate
                                    orderby MedicalRecords.DateOfVisit descending
                                    select new MedicalRecordsModel()
                                    {
                                        Id = MedicalRecords.Id,
                                        PatientId = MedicalRecords.PatientID,
                                        DoctorId = MedicalRecords.DoctorID,
                                        Diagnosis = MedicalRecords.Diagnosis,
                                        Treatment = MedicalRecords.Treatment,
                                        DateOfVisit = MedicalRecords.DateOfVisit
                                    }).ToListAsync();

                if (querys == null || querys.Count == 0)
                {
                    results.Add(new OperationResult
                    {
                        Success = false,
                        Message = "No se encontraron registros médicos en el rango de fechas especificado."
                    });
                    return results;
                }

                results.Add(new OperationResult
                {
                    Success = true,
                    Message = "Registros médicos obtenidos exitosamente.",
                    Data = querys
                });
            }
            catch (Exception ex)
            {
                var errorMessage = _configuration["ErrorMedicalRecordsRepository:GetMedicalRecordsByDateRange"]
                                   ?? "Error desconocido al obtener los registros médicos por rango de fechas.";
                _logger.LogError("{ErrorMessage} - Exception: {Exception}", errorMessage, ex);
                results.Add(new OperationResult
                {
                    Success = false,
                    Message = errorMessage
                });
            }

            return results;
        }

        public async Task<OperationResult> DeleteMedicalRecordAsync(int id)
        {
            //Validar si el ID es válido
            if (id <= 0)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "El ID del historial médico no es válido."
                };
            }

            //Buscar el registro en la base de datos
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record == null)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Historial médico no encontrado."
                };
            }

            //Eliminar el registro
            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Historial médico eliminado correctamente." };
        }

        public override async Task<OperationResult> SaveEntityAsync(MedicalRecords entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _context.MedicalRecords.Add(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Registro médico guardado exitosamente.";
            }
            catch (DbUpdateException dbEx)
            {
                result.Success = false;
                result.Message = $"Error de actualización de la base de datos: {dbEx.Message}";
                _logger.LogError(dbEx, "Error de actualización de la base de datos al guardar el registro médico.");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error inesperado: {ex.Message}";
                _logger.LogError(ex, "Error inesperado al guardar el registro médico.");
            }
            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(int Id, MedicalRecords entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _context.MedicalRecords.Update(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Registro médico actualizado exitosamente.";
            }
            catch (DbUpdateException dbEx)
            {
                result.Success = false;
                result.Message = $"Error de actualización de la base de datos: {dbEx.Message}";
                _logger.LogError(dbEx, "Error de actualización de la base de datos al actualizar el registro médico.");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error inesperado: {ex.Message}";
                _logger.LogError(ex, "Error inesperado al actualizar el registro médico.");
            }
            return result;
        }
    }
}
