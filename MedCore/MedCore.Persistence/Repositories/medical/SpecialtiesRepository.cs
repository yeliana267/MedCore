using MedCore.Domain.Base;
using MedCore.Domain.Entities;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.medical;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace MedCore.Persistence.Repositories.medical
{
    public class SpecialtiesRepository : BaseRepository<Specialties, short>, ISpecialtiesRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<SpecialtiesRepository> _logger;
        private readonly IConfiguration _configuration;

        public SpecialtiesRepository(MedCoreContext context, ILogger<SpecialtiesRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        private bool ValidateSpecialtyNameNotEmpty(Specialties entity)
        {
            return !string.IsNullOrWhiteSpace(entity.SpecialtyName);
        }

        private bool ValidateSpecialtyNameNoSpecialChars(Specialties entity)
        {
            return !Regex.IsMatch(entity.SpecialtyName, @"[^a-zA-Z\s]");
        }

        private bool ValidateSpecialtyNameMinLength(Specialties entity)
        {
            return entity.SpecialtyName.Length >= 3;
        }

        private async Task<bool> ValidateSpecialtyNameUniqueAsync(Specialties entity)
        {
            try
            {
                return !await _context.Specialties.AnyAsync(e => e.SpecialtyName == entity.SpecialtyName && e.Id != entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating uniqueness of SpecialtyName.");
                throw;
            }
        }

        public override async Task<OperationResult> SaveEntityAsync(Specialties entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (!ValidateSpecialtyNameNotEmpty(entity))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad no puede estar vacío.";
                    return result;
                }

                if (!ValidateSpecialtyNameNoSpecialChars(entity))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad no puede contener caracteres especiales.";
                    return result;
                }

                if (!ValidateSpecialtyNameMinLength(entity))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad debe tener al menos 3 caracteres.";
                    return result;
                }

                if (!await ValidateSpecialtyNameUniqueAsync(entity))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad ya existe.";
                    return result;
                }

                return await base.SaveEntityAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Specialty.");
                result.Success = false;
                result.Message = $"Ocurrió un error guardando la especialidad: {ex.Message}";
                return result;
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(Specialties entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                if (!ValidateSpecialtyNameNotEmpty(entity))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad no puede estar vacío.";
                    return result;
                }

                if (!ValidateSpecialtyNameNoSpecialChars(entity))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad no puede contener caracteres especiales.";
                    return result;
                }

                if (!ValidateSpecialtyNameMinLength(entity))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad debe tener al menos 3 caracteres.";
                    return result;
                }

                if (!await ValidateSpecialtyNameUniqueAsync(entity))
                {
                    result.Success = false;
                    result.Message = "El nombre de la especialidad ya existe.";
                    return result;
                }

                return await base.UpdateEntityAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Specialty.");
                result.Success = false;
                result.Message = $"Ocurrió un error actualizando la especialidad: {ex.Message}";
                return result;
            }
        }
    }
}
