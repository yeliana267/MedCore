using MedCore.Domain.Base;
using MedCore.Domain.Entities;
using MedCore.Model.Models.medical;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.medical
{
    public class SpecialtiesRepository : BaseRepository<Specialties, short>, ISpecialtiesRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<SpecialtiesRepository> _logger;
        private readonly IConfiguration _configuration;

        public SpecialtiesRepository(MedCoreContext context, ILogger<SpecialtiesRepository> logger, IConfiguration configuration) : base(context)
        {
            this._context = context;
            this._logger = logger;
            this._configuration = configuration;
        }

        public async Task<OperationResult> GetSpecialtyByNameAsync(string name)
        {
            var result = new OperationResult();

            try
            {
                // Verificar si el nombre es nulo o vacío
                if (string.IsNullOrWhiteSpace(name))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad no puede estar vacío.";
                    return result;
                }

                // Normalizar el nombre para evitar errores de formato
                name = name.Trim().ToLower();

                // Verificar si el contexto de Specialties está disponible
                if (_context.Specialties == null)
                {
                    result.Success = false;
                    result.Message = "Error: No se pudo acceder a la base de datos de especialidades.";
                    return result;
                }

                // Buscar la especialidad con el nombre normalizado
                var specialty = await _context.Specialties
                    .FirstOrDefaultAsync(s => s.SpecialtyName.ToLower() == name);

                // Verificar si se encontró la especialidad
                if (specialty == null)
                {
                    result.Success = false;
                    result.Message = "No se encontró la especialidad solicitada.";
                    return result;
                }

                // Retornar el resultado encontrado
                result.Success = true;
                result.Data = specialty;
                return result;
            }
            catch (Exception ex)
            {
                var errorMessage = _configuration["ErrorSpecialtiesRepository:GetSpecialtyByNameAsync"]
                                   ?? "Error desconocido al obtener la especialidad por nombre.";

                _logger.LogError("{ErrorMessage} - Exception: {Exception}", errorMessage, ex);

                result.Success = false;
                result.Message = errorMessage;
                return result;
            }
        }

        public async Task<OperationResult> DeleteSpecialtyAsync(short id)
        {
            OperationResult result = new OperationResult();

            try
            {
                // Validación: ID válido
                if (id <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID de la especialidad no es válido.";
                    return result;
                }

                // Validación: Acceso al contexto
                if (_context.Specialties == null)
                {
                    result.Success = false;
                    result.Message = "No se pudo acceder a la base de datos de especialidades.";
                    return result;
                }

                // Buscar especialidad por ID
                var specialty = await _context.Specialties.FindAsync(id);
                if (specialty == null)
                {
                    result.Success = false;
                    result.Message = "Especialidad no encontrada.";
                    return result;
                }

                // Validación: Verificar si ya está inactiva o eliminada lógicamente (si aplica)
                if (!specialty.IsActive)
                {
                    result.Success = false;
                    result.Message = "La especialidad ya se encuentra inactiva o eliminada.";
                    return result;
                }

                // Eliminación lógica (opcional)
                // specialty.IsActive = false;
                // _context.Specialties.Update(specialty);

                // Eliminación física (como ya tienes)
                _context.Specialties.Remove(specialty);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = "Especialidad eliminada correctamente.";
            }
            catch (DbUpdateException dbEx)
            {
                result.Success = false;
                result.Message = "No se pudo eliminar la especialidad debido a restricciones de integridad referencial.";
                _logger.LogError(dbEx, "Error al eliminar la especialidad con ID {Id}", id);
            }
            catch (Exception ex)
            {
                var errorMessage = _configuration["ErrorSpecialtiesRepository:DeleteSpecialtyAsync"]
                                   ?? "Error desconocido al eliminar la especialidad.";

                result.Success = false;
                result.Message = errorMessage;
                _logger.LogError(ex, errorMessage);
            }

            return result;
        }

        public override async Task<OperationResult> SaveEntityAsync(Specialties entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                // Validación: Verificar que el objeto no sea nulo
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "La entidad 'Specialties' no puede ser nula.";
                    return result;
                }

                // Validación: Nombre requerido
                if (string.IsNullOrWhiteSpace(entity.SpecialtyName))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad es obligatorio.";
                    return result;
                }

                // Validación: Longitud mínima del nombre
                if (entity.SpecialtyName.Trim().Length < 3)
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad debe tener al menos 3 caracteres.";
                    return result;
                }

                // Validación: Evitar duplicados (case insensitive)
                bool exists = await _context.Specialties
                    .AnyAsync(s => s.SpecialtyName.ToLower().Trim() == entity.SpecialtyName.ToLower().Trim());

                if (exists)
                {
                    result.Success = false;
                    result.Message = "Ya existe una especialidad con ese nombre.";
                    return result;
                }

                // Normalización del nombre
                entity.SpecialtyName = entity.SpecialtyName.Trim();

                // Asignación de estado activo si no viene definido
                if (!entity.IsActive)
                {
                    entity.IsActive = true;
                }

                // Guardar la entidad
                _context.Specialties.Add(entity);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = "Especialidad guardada exitosamente.";
            }
            catch (DbUpdateException dbEx)
            {
                result.Success = false;
                result.Message = "Error al guardar la especialidad. Puede estar relacionada con otra entidad.";
                _logger.LogError(dbEx, "Error al guardar la especialidad: {Message}", dbEx.Message);
            }
            catch (Exception ex)
            {
                var errorMessage = _configuration["ErrorSpecialtiesRepository:SaveEntityAsync"]
                                   ?? "Ocurrió un error desconocido al guardar la especialidad.";

                result.Success = false;
                result.Message = errorMessage;
                _logger.LogError(ex, errorMessage);
            }

            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(short Id, Specialties entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                // Validación: Verificar si el ID es válido
                if (Id <= 0)
                {
                    result.Success = false;
                    result.Message = "El ID de la especialidad no es válido.";
                    return result;
                }

                // Validación: Verificar que la entidad no sea nula
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "La entidad 'Specialties' no puede ser nula.";
                    return result;
                }

                // Validación: Nombre de especialidad obligatorio
                if (string.IsNullOrWhiteSpace(entity.SpecialtyName) || entity.SpecialtyName.Trim().Length < 3)
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad es obligatorio y debe tener al menos 3 caracteres.";
                    return result;
                }

                // Normalizar el nombre para evitar inconsistencias
                entity.SpecialtyName = entity.SpecialtyName.Trim();

                // Buscar la especialidad en la base de datos
                var existingSpecialty = await _context.Specialties.FindAsync(Id);
                if (existingSpecialty == null)
                {
                    result.Success = false;
                    result.Message = "No se encontró la especialidad a actualizar.";
                    return result;
                }

                // Verificar si el nuevo nombre ya existe en otra especialidad
                bool exists = await _context.Specialties
                    .AnyAsync(s => s.Id != Id && s.SpecialtyName.ToLower() == entity.SpecialtyName.ToLower());

                if (exists)
                {
                    result.Success = false;
                    result.Message = "Ya existe otra especialidad con el mismo nombre.";
                    return result;
                }

                // Actualizar los valores permitidos
                existingSpecialty.SpecialtyName = entity.SpecialtyName;
                existingSpecialty.IsActive = entity.IsActive;

                _logger.LogInformation($"Actualizando especialidad {existingSpecialty.Id} - {existingSpecialty.SpecialtyName}");

                // Guardar los cambios
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = "Especialidad actualizada exitosamente.";
            }
            catch (DbUpdateException dbEx)
            {
                result.Success = false;
                result.Message = "Error al actualizar la especialidad. Puede estar relacionada con otra entidad.";
                _logger.LogError(dbEx, "Error al actualizar la especialidad con ID {Id}", Id);
            }
            catch (Exception ex)
            {
                var errorMessage = _configuration["ErrorSpecialtiesRepository:UpdateEntityAsync"]
                                   ?? "Ocurrió un error inesperado al actualizar la especialidad.";

                result.Success = false;
                result.Message = errorMessage;
                _logger.LogError(ex, errorMessage);
            }

            return result;
        }
    }
}

