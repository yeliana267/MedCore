using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
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

        public async Task<List<OperationResult>> GetRecentlyUpdatedModesAsync(int days)
        {
            List<OperationResult> results = new List<OperationResult>();
            try
            {
                var recentDate = DateTime.Now.AddDays(-days);
                var modes = await _context.AvailabilityModes
                                          .Where(am => am.UpdatedAt >= recentDate)
                                          .ToListAsync();

                if (modes == null || modes.Count == 0)
                {
                    results.Add(new OperationResult
                    {
                        Success = false,
                        Message = "No se encontraron modos de disponibilidad actualizados recientemente."
                    });
                    return results;
                }

                results.Add(new OperationResult
                {
                    Data = modes,
                    Success = true,
                    Message = "Modos de disponibilidad actualizados recientemente obtenidos exitosamente."
                });
            }
            catch (Exception ex)
            {
                results.Add(new OperationResult
                {
                    Message = _configuration["ErrorAvailabilityModesRepository:GetRecentlyUpdatedModes"]
                             ?? "Error desconocido al obtener modos de disponibilidad actualizados recientemente.",
                    Success = false
                });
                _logger.LogError($"Error al obtener modos de disponibilidad actualizados recientemente", ex);
            }
            return results;
        }

        public async Task<OperationResult> GetAvailabilityModeByNameAsync(string name)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    result.Success = false;
                    result.Message = "El nombre del modo de disponibilidad no puede estar vacío.";
                    return result;
                }

                name = name.Trim().ToLower();

                var mode = await _context.AvailabilityModes
                                         .FirstOrDefaultAsync(am => am.AvailabilityMode.ToLower() == name);

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
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorAvailabilityModesRepository:GetAvailabilityModeByName"]
                                 ?? "Error desconocido al obtener el modo de disponibilidad por nombre.";
                result.Success = false;
                _logger.LogError($"Error al obtener el modo de disponibilidad por nombre: {name}", ex);
            }
            return result;
        }

        public async Task<OperationResult> DeleteAvailabilityModeAsync(short id)
        {
            //Validar si el ID es válido
            if (id <= 0)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "El ID del modo de disponibilidad no es válido."
                };
            }

            //Buscar el registro en la base de datos
            var mode = await _context.AvailabilityModes.FindAsync(id);
            if (mode == null)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Modo de disponibilidad no encontrado."
                };
            }

            //Eliminar el registro
            _context.AvailabilityModes.Remove(mode);
            await _context.SaveChangesAsync();

            return new OperationResult { Success = true, Message = "Modo de disponibilidad eliminado correctamente." };
        }

        public override async Task<OperationResult> SaveEntityAsync(AvailabilityModes entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                _context.AvailabilityModes.Add(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Entidad guardada exitosamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} guardando la entidad.";
            }
            return result;
        }


        public override Task<OperationResult> UpdateEntityAsync(AvailabilityModes entity)
        {
            _logger.LogInformation($"Actualizando modo de disponibilidad {entity.Id} - {entity.AvailabilityMode}");
            return base.UpdateEntityAsync(entity);
        }
    }
}