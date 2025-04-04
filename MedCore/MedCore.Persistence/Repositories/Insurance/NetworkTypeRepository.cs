﻿

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.Insurance;
using MedCore.Model.Models.Insurance;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Insurance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.Insurance
{
    public class NetworkTypeRepository : BaseRepository<NetworkType, int>, INetworkTypeRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<NetworkTypeRepository> _logger;
        private readonly IConfiguration _configuration;
        public NetworkTypeRepository(MedCoreContext context,
                                     ILogger<NetworkTypeRepository> logger,
                                     IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<OperationResult> GetNetworkTypeById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {

                var networkType = await _context.NetworkType.FindAsync(id);

                if (networkType == null)
                {
                    result.Success = false;
                    result.Message = "Network type not found.";
                    return result;
                }

                result.Success = true;
                result.Data = networkType;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error retrieving network type with ID {Id}", id);
                result.Success = false;
                result.Message = _configuration["An error occurred while fetching the network type."]!;
            }

            return result;

        }
        
        public async Task<OperationResult> GetNetworkTypeList()
        {
            OperationResult result = new OperationResult();

            try
            {

                var query = await (from networkType in _context.NetworkType
                                   select new NetworkTypeModel()
                                   { Name = networkType.Name }).ToListAsync();

                if (query.Count == 0)
                {
                    result.Message = "No NetworkType found list";
                }

                result.Data = query;

            }
            catch (Exception ex) 
            {

                result.Message = _configuration["ErrorNetworkTypeRepository:GetNetworkType"]!;
                _logger.LogError(result.Message,ex.ToString());
            }

            return result;

        }

       

        public override async Task<OperationResult> SaveEntityAsync(NetworkType entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if(entity == null)
                {
                    result.Success = false;
                    result.Message = "La entidad no puede ser nula.";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(entity.Name))
                {
                    result.Success = false;
                    result.Message = "Name no puede estar vacio.";
                    return result;
                }
                if (entity.Name.Length > 50)
                {
                    result.Success = false;
                    result.Message = "La longitud del nombre no puede exceder los 50 caracteres.";
                    return result;
                }
                if(_context.NetworkType.Any(de => de.Name == entity.Name))
                {
                    result.Success = false;
                    result.Message = "El networktype no puede estar duplicado";
                    return result;
                }
                if(entity.Description?.Length > 255)
                {
                    result.Success = false;
                    result.Message = "La longitud de la descripcion no puede exceder los 255 caracteres.";
                    return result;
                }
                _context.NetworkType.Add(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Entidad guardada exitosamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} guardando la entidad.";
                _logger.LogError(ex, "Error al guardar la entidad NetworkType.");
            }
            return result;
        }


        public override async Task<OperationResult> UpdateEntityAsync(int id, NetworkType entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "La entidad no puede ser nula.";
                    return result;
                }

                var networkType = await _context.NetworkType.FindAsync(id);

                if (networkType == null)
                {
                    result.Success = false;
                    result.Message = $"No se encontró NetworkType por ID {id}.";
                    return result;
                }
                if (string.IsNullOrWhiteSpace(entity.Name))
                {
                    result.Success = false;
                    result.Message = "Name no puede estar vacio.";
                    return result;

                }
                if (entity.Name.Length > 50)
                {
                    result.Success = false;
                    result.Message = "La longitud del nombre no puede exceder los 50 caracteres.";
                    return result;
                }
                if (entity.Description?.Length > 255)
                {
                    result.Success = false;
                    result.Message = "La longitud de la descripcion no puede exceder los 255 caracteres.";
                    return result;
                }

                _logger.LogInformation($"Actualizando NetworkType {entity.Id}");

                networkType.Name = entity.Name?? networkType.Name;
                networkType.Description = entity.Description?? networkType.Description;
                networkType.UpdatedAt = DateTime.Now;


                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = $"NetworkType con ID {id} actualizada correctamente.";
                _logger.LogInformation($"NetworkType {id} actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar NetworkType: {ex.Message}";
                _logger.LogError($"Error en UpdateEntityAsync para NetworkType {id}: {ex.Message}", ex);
            }

            return result;
        }


        public override async Task<OperationResult> DeleteEntityByIdAsync(int NetworkTypeId)
        {
            OperationResult result = new OperationResult();

            try
            {
     
                var querys = await _context.Database.ExecuteSqlRawAsync("DELETE FROM [MedicalAppointment].[Insurance].[NetworkType] Where NetworkTypeId = {0}", NetworkTypeId);



                if (querys > 0)
                {
                    result.Success = true;
                    result.Message = "Tipo de Red de Seguro borrada exitosamente";
                }
                else
                {
                    result.Success = false;
                    result.Message = "No se encontró Tipo de Red de Seguro con ese Id";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al eliminar Red de Seguro.";
                _logger.LogError($"Error al eliminar Tipo de Red de Seguro por ID {NetworkTypeId}: {ex.Message}", ex);
            }
            return result;

        }

    }


}

