

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.users;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.users
{
    public class PatientsRepository : BaseRepository<Patients, int>, IPatientsRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<PatientsRepository> _logger;
        private readonly IConfiguration _configuration;
        public PatientsRepository(MedCoreContext context, ILogger<PatientsRepository> loger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = loger;
            _configuration = configuration;
        }


        public override async Task<OperationResult> DeleteEntityByIdAsync(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var querys = await _context.Database.ExecuteSqlRawAsync(
            "DELETE FROM [MedicalAppointment].[users].[Patients] WHERE PatientID = {0}", id);

                if (querys > 0)
                {
                    result.Success = true;
                    result.Message = "Paciente eliminado exitosamente.";
                }
                else
                {
                    result.Success = false;
                    result.Message = "No se encontró ningún paciente con ese ID.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al eliminar el paciente.";
                _logger.LogError($"Error al eliminar el paciente con ID {id}: {ex.Message}", ex);
            }
            return result;
        }

        public Task<OperationResult> GetPatientsByBloodTypeAsync(string bloodType)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetPatientsByDoctorIdAsync(int doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateEmergencyContactAsync(int patientId, string contactName, string contactPhone)
        {
            throw new NotImplementedException();
        }

        public override async Task<OperationResult> UpdateEntityAsync(int id, Patients entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                var patients = await _context.Patients.FindAsync(id);

                if (patients == null)
                {
                    result.Success = false;
                    result.Message = $"No se encontró el paciente con ID {id}.";
                    _logger.LogWarning($"Intento de actualización fallido: paciente con id: {id} no encontrado.");
                    return result;
                }

                _logger.LogInformation($"Actualizando paciente con id: {id}");

                patients.PhoneNumber = entity.PhoneNumber;
                patients.Address = entity.Address;
                patients.DateOfBirth = entity.DateOfBirth;
                patients.Allergies = entity.Allergies;
                patients.InsuranceProviderID = entity.InsuranceProviderID;
                patients.BloodType = entity.BloodType;
                patients.EmergencyContactName = entity.EmergencyContactName;
                patients.EmergencyContactPhone = entity.EmergencyContactPhone;


                _context.Patients.Update(patients);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = $"Paciente con ID {id} actualizada correctamente.";
                _logger.LogInformation($"Paciente {id} actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar el paciente: {ex.Message}";
                _logger.LogError($"Error en UpdateDoctorAsync para el doctor {id}: {ex.Message}", ex);
            }

            return result;
        }

    }
}
