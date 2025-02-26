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

        public async Task<List<OperationResult>> GetRecentlyUpdatedModesAsync(int days)
        {
            try
            {
                var recentDate = DateTime.Now.AddDays(-days);
                var modes = await _context.AvailabilityModes
                                          .Where(am => am.UpdatedAt >= recentDate)
                                          .ToListAsync();

                var results = modes.Select(mode => new OperationResult
                {
                    Success = true,
                    Data = mode
                }).ToList();

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving recently updated availability modes.");
                return new List<OperationResult>
                {
                    new OperationResult
                    {
                        Success = false,
                        Message = "Error retrieving recently updated availability modes."
                    }
                };
            }
        }

        public async Task<OperationResult> GetAvailabilityModeByNameAsync(string name)
        {
            //Verificar si el nombre es nulo o vacío
            if (string.IsNullOrWhiteSpace(name))
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "El nombre del modo de disponibilidad no puede estar vacío."
                };
            }

            //Normalizar el nombre para evitar errores de formato
            name = name.Trim().ToLower();

            var mode = await _context.AvailabilityModes
                .FirstOrDefaultAsync(am => am.AvailabilityMode.ToLower() == name);

            if (mode == null)
            {
                return new OperationResult { Success = false, Message = "Modo de disponibilidad no encontrado." };
            }

            return new OperationResult { Success = true, Data = mode };
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
                result.Message = "Modo de disponibilidad guardado exitosamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error guardando el modo de disponibilidad: {ex.Message}";
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