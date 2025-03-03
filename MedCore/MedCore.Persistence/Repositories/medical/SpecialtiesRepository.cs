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

        public async Task<List<OperationResult>> GetActiveSpecialtiesAsync()
        {
            List<OperationResult> results = new List<OperationResult>();

            try
            {
                //Verificar si el contexto de Specialties está disponible
                if (_context.Specialties == null)
                {
                    results.Add(new OperationResult
                    {
                        Success = false,
                        Message = "Error: No se pudo acceder a la base de datos de especialidades."
                    });
                    return results;
                }

                var querys = await (from Specialties in _context.Specialties
                                    where Specialties.IsActive == true
                                    select new SpecialtiesModel()
                                    {
                                        Id = Specialties.Id,
                                        SpecialtyName = Specialties.SpecialtyName,  
                                        IsActive = Specialties.IsActive  
                                    }).ToListAsync();

                //Verificar si hay especialidades activas antes de agregarlas
                if (querys == null || querys.Count == 0)
                {
                    results.Add(new OperationResult
                    {
                        Success = false,
                        Message = "No existen especialidades activas."
                    });
                    return results;
                }

                results.Add(new OperationResult
                {
                    Success = true,
                    Data = querys
                });

                return results;
            }
            catch (Exception ex)
            {
                var errorMessage = _configuration["ErrorSpecialtiesRepository:GetActiveSpecialtiesAsync"]
                                   ?? "Error desconocido al obtener especialidades activas.";

                _logger.LogError("{ErrorMessage} - Exception: {Exception}", errorMessage, ex);

                results.Add(new OperationResult
                {
                    Success = false,
                    Message = errorMessage
                });

                return results;
            }
        }

        public async Task<OperationResult> GetSpecialtyByNameAsync(string name)
        {
            OperationResult result = new OperationResult();

            try
            {
                //Verificar si el nombre es nulo o vacío
                if (string.IsNullOrWhiteSpace(name))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad no puede estar vacío.";
                    return result;
                }

                //Normalizar el nombre para evitar errores de formato
                name = name.Trim().ToLower();

                //Verificar si el contexto de Specialties está disponible
                if (_context.Specialties == null)
                {
                    result.Success = false;
                    result.Message = "Error: No se pudo acceder a la base de datos de especialidades.";
                    return result;
                }

                //Buscar la especialidad con el nombre normalizado
                var specialty = await _context.Specialties
                    .Where(s => s.SpecialtyName.ToLower() == name)
                    .ToListAsync();

                // Verificar si hay resultados
                if (specialty == null || specialty.Count == 0)
                {
                    result.Success = false;
                    result.Message = "No se encontró la especialidad solicitada.";
                    return result;
                }

                //Verificar si hay múltiples especialidades con el mismo nombre
                if (specialty.Count > 1)
                {
                    result.Success = false;
                    result.Message = "Se encontraron múltiples especialidades con el mismo nombre.";
                    return result;
                }

                // Retornar el primer (y único) resultado encontrado
                result.Success = true;
                result.Data = specialty.First();
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
            //Validar si el ID es válido
            if (id <= 0)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "El ID de la especialidad no es válido."
                };
            }

            //Buscar el registro en la base de datos
            var specialty = await _context.Specialties.FindAsync(id);
            if (specialty == null)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Especialidad no encontrada."
                };
            }

            //Eliminar el registro
            _context.Specialties.Remove(specialty);
            await _context.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Especialidad eliminada correctamente." };
        }

        public override async Task<OperationResult> SaveEntityAsync(Specialties entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _context.Specialties.Add(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Especialidad guardada exitosamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} guardando la especialidad.";
            }
            return result;
        }

        public override Task<OperationResult> UpdateEntityAsync(short Id, Specialties entity)
        {
            _logger.LogInformation($"Actualizando especialidad {entity.Id} - {entity.SpecialtyName}");
            return base.UpdateEntityAsync(Id, entity);
        }
    }
}

