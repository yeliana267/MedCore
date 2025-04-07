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
        private readonly MedCoreContext _context;
        private readonly ILogger<AvailabilityModesRepository> _logger;
        private readonly IConfiguration _configuration;
        public AvailabilityModesRepository(MedCoreContext context, ILogger<AvailabilityModesRepository> logger, IConfiguration configuration) : base(context)
        {
            this._context = context;
            this._logger = logger;
            this._configuration = configuration;
        }

        public async Task<OperationResult> GetAvailabilityModeByNameAsync(string name)
        {
            OperationResult result = new OperationResult();

            try
            {
                // Validar el parámetro de entrada (1)
                if (string.IsNullOrWhiteSpace(name))
                {
                    result.Success = false;
                    result.Message = "El nombre del modo de disponibilidad no puede estar vacío.";
                    return result;
                }

                name = name.Trim().ToLower();

                // Validar si el contexto está disponible (2)
                if (_context == null)
                {
                    result.Success = false;
                    result.Message = "Error interno: El contexto de base de datos no está disponible.";
                    return result;
                }

                // Validar la conexión a la base de datos (3)
                if (!_context.Database.CanConnect())
                {
                    result.Success = false;
                    result.Message = "Error: No se puede conectar a la base de datos.";
                    return result;
                }

                // Validar que la entidad AvailabilityModes exista en el contexto (4)
                if (!_context.Model.GetEntityTypes().Any(e => e.ClrType == typeof(AvailabilityModes)))
                {
                    result.Success = false;
                    result.Message = "Error: La entidad AvailabilityModes no está configurada en el contexto.";
                    return result;
                }

                // Obtener el modo de disponibilidad por nombre
                var mode = await _context.AvailabilityModes
                                         .FirstOrDefaultAsync(am => am.AvailabilityMode.ToLower() == name);

                // Validar si se encontró el registro (5)
                if (mode == null)
                {
                    result.Success = false;
                    result.Message = "Modo de disponibilidad no encontrado.";
                    return result;
                }

                result.Data = mode;
                result.Success = true;
                result.Message = "Modo de disponibilidad obtenido exitosamente.";
            }
            catch (ArgumentNullException ex)
            {
                result.Success = false;
                result.Message = "Error: Se recibió un valor nulo inesperado.";
                _logger.LogError(ex, "ArgumentNullException en GetAvailabilityModeByNameAsync");
            }
            catch (InvalidOperationException ex)
            {
                result.Success = false;
                result.Message = "Error: Operación inválida al acceder a la base de datos.";
                _logger.LogError(ex, "InvalidOperationException en GetAvailabilityModeByNameAsync");
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAvailabilityModesRepository:GetAvailabilityModeByName"]
                                 ?? "Error desconocido al obtener el modo de disponibilidad por nombre.";
                result.Success = false;
                _logger.LogError(ex, $"Error inesperado al obtener el modo de disponibilidad por nombre: {name}");
            }

            return result;
        }


        public async Task<OperationResult> GetAvailabilityModeByIdAsync(short id)
        {
            var result = new OperationResult();

            try
            {
                // Validar si el ID es válido (1)
                if (id <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID proporcionado no es válido.";
                    return result;
                }

                // Validar si el contexto está disponible (2)
                if (_context == null)
                {
                    result.Success = false;
                    result.Message = "Error interno: El contexto de base de datos no está disponible.";
                    return result;
                }

                // Validar la conexión a la base de datos (3)
                if (!_context.Database.CanConnect())
                {
                    result.Success = false;
                    result.Message = "Error: No se puede conectar a la base de datos.";
                    return result;
                }

                // Validar que la entidad AvailabilityModes exista en el contexto (4)
                if (!_context.Model.GetEntityTypes().Any(e => e.ClrType == typeof(AvailabilityModes)))
                {
                    result.Success = false;
                    result.Message = "Error: La entidad AvailabilityModes no está configurada en el contexto.";
                    return result;
                }

                // Buscar el modo de disponibilidad por ID
                var availabilityMode = await _context.AvailabilityModes.FindAsync(id);

                // Validar si se encontró el registro (5)
                if (availabilityMode == null)
                {
                    result.Success = false;
                    result.Message = "No se encontró el modo de disponibilidad con el ID proporcionado.";
                    return result;
                }

                result.Data = availabilityMode;
                result.Success = true;
                result.Message = "Modo de disponibilidad obtenido exitosamente.";
            }
            catch (ArgumentNullException ex)
            {
                result.Success = false;
                result.Message = "Error: Se recibió un valor nulo inesperado.";
                _logger.LogError(ex, "ArgumentNullException en GetAvailabilityModeByIdAsync");
            }
            catch (InvalidOperationException ex)
            {
                result.Success = false;
                result.Message = "Error: Operación inválida al acceder a la base de datos.";
                _logger.LogError(ex, "InvalidOperationException en GetAvailabilityModeByIdAsync");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error desconocido al obtener el modo de disponibilidad: {ex.Message}";
                _logger.LogError(ex, $"Error inesperado al obtener el modo de disponibilidad con ID {id}: {ex.Message}");
            }

            return result;
        }

        public override async Task<OperationResult> SaveEntityAsync(AvailabilityModes entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                // Validar si la entidad es nula (1)
                if (entity == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "La entidad no puede ser nula."
                    };
                }

                // Validar si el contexto está disponible (2)
                if (_context == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Error interno: El contexto de base de datos no está disponible."
                    };
                }

                // Validar la conexión a la base de datos (3)
                if (!_context.Database.CanConnect())
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Error: No se puede conectar a la base de datos."
                    };
                }

                // Validar que la entidad AvailabilityModes exista en el contexto (4)
                if (!_context.Model.GetEntityTypes().Any(e => e.ClrType == typeof(AvailabilityModes)))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Error: La entidad AvailabilityModes no está configurada en el contexto."
                    };
                }

                // Validar campos obligatorios (5)
                if (string.IsNullOrWhiteSpace(entity.AvailabilityMode))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El campo 'AvailabilityMode' es obligatorio."
                    };
                }

                // Guardar la entidad
                _context.AvailabilityModes.Add(entity);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = "Entidad guardada exitosamente.";
                result.Data = entity;
            }
            catch (DbUpdateException ex)
            {
                result.Success = false;
                result.Message = "Error al guardar la entidad. Puede estar relacionado con restricciones de la base de datos.";
                _logger.LogError(ex, "DbUpdateException en SaveEntityAsync");
            }
            catch (InvalidOperationException ex)
            {
                result.Success = false;
                result.Message = "Error de operación inválida al guardar la entidad.";
                _logger.LogError(ex, "InvalidOperationException en SaveEntityAsync");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error desconocido al guardar la entidad: {ex.Message}";
                _logger.LogError(ex, "Error inesperado en SaveEntityAsync");
            }

            return result;
        }


        public override async Task<OperationResult> UpdateEntityAsync(short Id, AvailabilityModes entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                // Validar si el ID es válido (1)
                if (Id <= 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El ID proporcionado no es válido."
                    };
                }

                // Validar si la entidad es nula (2)
                if (entity == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "La entidad no puede ser nula."
                    };
                }

                // Validar si el contexto está disponible (3)
                if (_context == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Error interno: El contexto de base de datos no está disponible."
                    };
                }

                // Validar la conexión a la base de datos (4)
                if (!_context.Database.CanConnect())
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Error: No se puede conectar a la base de datos."
                    };
                }

                // Validar que la entidad AvailabilityModes exista en el contexto (5)
                if (!_context.Model.GetEntityTypes().Any(e => e.ClrType == typeof(AvailabilityModes)))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Error: La entidad AvailabilityModes no está configurada en el contexto."
                    };
                }

                // Buscar la entidad a actualizar (6)
                var existingEntity = await _context.AvailabilityModes.FindAsync(Id);
                if (existingEntity == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "No se encontró el modo de disponibilidad a actualizar."
                    };
                }

                // Validar campos obligatorios (7)
                if (string.IsNullOrWhiteSpace(entity.AvailabilityMode))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "El campo 'AvailabilityMode' es obligatorio."
                    };
                }

                // Actualizar la entidad
                _logger.LogInformation($"Actualizando modo de disponibilidad {entity.Id} - {entity.AvailabilityMode}");

                existingEntity.AvailabilityMode = entity.AvailabilityMode;
                existingEntity.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new OperationResult
                {
                    Success = true,
                    Message = "Entidad actualizada exitosamente.",
                    Data = existingEntity
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DbUpdateException al actualizar el modo de disponibilidad con ID {Id}", Id);
                return new OperationResult
                {
                    Success = false,
                    Message = "Error al actualizar la entidad. Puede estar relacionado con restricciones de la base de datos."
                };
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "InvalidOperationException al actualizar el modo de disponibilidad con ID {Id}", Id);
                return new OperationResult
                {
                    Success = false,
                    Message = "Error de operación inválida al actualizar la entidad."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar el modo de disponibilidad con ID {Id}", Id);
                return new OperationResult
                {
                    Success = false,
                    Message = $"Error desconocido al actualizar la entidad: {ex.Message}"
                };
            }
        }
    }
}