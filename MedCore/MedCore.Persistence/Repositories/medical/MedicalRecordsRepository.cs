using MedCore.Domain.Base;
using MedCore.Domain.Entities.medical;
using MedCore.Domain.Entities.users;
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

            // Validar si el ID del paciente es válido 
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
                // Validar si el contexto está disponible 
                if (_context == null)
                {
                    results.Add(new OperationResult
                    {
                        Success = false,
                        Message = "Error interno: El contexto de base de datos no está disponible."
                    });
                    return results;
                }

                // Validar conexión a la base de datos 
                if (!_context.Database.CanConnect())
                {
                    results.Add(new OperationResult
                    {
                        Success = false,
                        Message = "Error: No se puede conectar a la base de datos."
                    });
                    return results;
                }

                // Validar existencia de las entidades en el modelo 
                var entityTypes = _context.Model.GetEntityTypes().Select(e => e.ClrType).ToList();
                if (!entityTypes.Contains(typeof(MedicalRecords)) || !entityTypes.Contains(typeof(Patients)) || !entityTypes.Contains(typeof(Doctors)))
                {
                    results.Add(new OperationResult
                    {
                        Success = false,
                        Message = "Error interno: Las entidades necesarias no están configuradas en el contexto."
                    });
                    return results;
                }

                // Consultar los registros médicos 
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

                // Verificar si no existen registros médicos para el paciente 
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
            catch (DbUpdateException ex)
            {
                var errorMessage = _configuration["ErrorMedicalRecordsRepository:GetMedicalRecordsByPatient"]
                                   ?? "Error de actualización al obtener el historial médico.";
                _logger.LogError("{ErrorMessage} - DbUpdateException: {Exception}", errorMessage, ex);
                results.Add(new OperationResult
                {
                    Success = false,
                    Message = errorMessage
                });
            }
            catch (InvalidOperationException ex)
            {
                var errorMessage = "Error de operación inválida al obtener el historial médico.";
                _logger.LogError("{ErrorMessage} - InvalidOperationException: {Exception}", errorMessage, ex);
                results.Add(new OperationResult
                {
                    Success = false,
                    Message = errorMessage
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


        public async Task<OperationResult> DeleteMedicalRecordAsync(int id)
        {
            try
            {
                // Validar si el ID es válido 
                if (id <= 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El ID del historial médico no es válido."
                    };
                }

                // Validar el contexto de base de datos 
                if (_context == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Error interno: El contexto de base de datos no está disponible."
                    };
                }

                // Verificar la conexión a la base de datos 
                if (!_context.Database.CanConnect())
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Error: No se puede conectar a la base de datos."
                    };
                }

                // Validar la existencia de la entidad MedicalRecords en el modelo 
                var entityTypes = _context.Model.GetEntityTypes().Select(e => e.ClrType).ToList();
                if (!entityTypes.Contains(typeof(MedicalRecords)))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Error interno: La entidad MedicalRecords no está configurada en el contexto."
                    };
                }

                // Buscar el registro en la base de datos 
                var record = await _context.MedicalRecords.FindAsync(id);
                if (record == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Historial médico no encontrado."
                    };
                }

                // Eliminar el registro
                _context.MedicalRecords.Remove(record);
                await _context.SaveChangesAsync();

                return new OperationResult
                {
                    Success = true,
                    Message = "Historial médico eliminado correctamente."
                };
            }
            catch (DbUpdateException ex)
            {
                var errorMessage = _configuration["ErrorMedicalRecordsRepository:DeleteMedicalRecord"]
                                   ?? "Error al eliminar el historial médico.";
                _logger.LogError("{ErrorMessage} - DbUpdateException: {Exception}", errorMessage, ex);

                return new OperationResult
                {
                    Success = false,
                    Message = errorMessage
                };
            }
            catch (InvalidOperationException ex)
            {
                var errorMessage = "Error de operación inválida al eliminar el historial médico.";
                _logger.LogError("{ErrorMessage} - InvalidOperationException: {Exception}", errorMessage, ex);

                return new OperationResult
                {
                    Success = false,
                    Message = errorMessage
                };
            }
            catch (Exception ex)
            {
                var errorMessage = _configuration["ErrorMedicalRecordsRepository:DeleteMedicalRecord"]
                                   ?? "Error desconocido al eliminar el historial médico.";
                _logger.LogError("{ErrorMessage} - Exception: {Exception}", errorMessage, ex);

                return new OperationResult
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public override async Task<OperationResult> SaveEntityAsync(MedicalRecords entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                // Validar que la entidad no sea nula
                if (entity == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "La entidad MedicalRecords no puede ser nula."
                    };
                }

                // Validar campos obligatorios
                if (entity.PatientID <= 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El ID del paciente es obligatorio y debe ser válido."
                    };
                }

                if (entity.DoctorID <= 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El ID del doctor es obligatorio y debe ser válido."
                    };
                }

                if (string.IsNullOrWhiteSpace(entity.Diagnosis))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El diagnóstico es un campo obligatorio."
                    };
                }

                if (entity.DateOfVisit == default)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "La fecha de la visita es obligatoria."
                    };
                }

                // Validar el contexto de base de datos
                if (_context == null || !_context.Database.CanConnect())
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Error interno: No se puede acceder a la base de datos."
                    };
                }

                // Guardar la entidad
                await _context.MedicalRecords.AddAsync(entity);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = "Registro médico guardado exitosamente.";
                result.Data = entity;
            }
            catch (DbUpdateException dbEx)
            {
                result.Success = false;
                result.Message = "Error de actualización de la base de datos.";
                _logger.LogError(dbEx, "Error de actualización de la base de datos al guardar el registro médico.");
            }
            catch (InvalidOperationException ioEx)
            {
                result.Success = false;
                result.Message = "Operación no válida al guardar el registro médico.";
                _logger.LogError(ioEx, "Operación no válida al guardar el registro médico.");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrió un error inesperado al guardar el registro médico.";
                _logger.LogError(ex, "Error inesperado al guardar el registro médico.");
            }

            return result;
        }


        public override async Task<OperationResult> UpdateEntityAsync(int Id, MedicalRecords entity)
{
    OperationResult result = new OperationResult();

    try
    {
        // Verificar que la entidad no sea null
        if (entity == null)
        {
            result.Success = false;
            result.Message = "La entidad proporcionada es nula.";
            return result;
        }

        // Verificar que el ID proporcionado coincida con el de la entidad
        if (Id != entity.Id)
        {
            result.Success = false;
            result.Message = "El ID proporcionado no coincide con el ID de la entidad.";
            return result;
        }

        //Verificar que el registro exista en la base de datos
        var existingRecord = await _context.MedicalRecords.FindAsync(Id);
        if (existingRecord == null)
        {
            result.Success = false;
            result.Message = $"No se encontró un registro médico con el ID {Id}.";
            return result;
        }

        // Guardar cambios
        _context.MedicalRecords.Update(existingRecord);
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
