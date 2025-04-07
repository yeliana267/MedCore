using MedCore.Application.Dtos.users.Patients;
using MedCore.Domain.Base;
using MedCore.Web.Interfaces.users;
using MedCore.Web.Interfaces;
using MedCore.Web.Models.users.Patients;

namespace MedCore.Web.Repositories.usersWeb.Patients
{
    public class PatientsWeb : IPatientsWeb
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<PatientsWeb> _logger;

        public PatientsWeb(IApiClient apiClient, ILogger<PatientsWeb> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<List<PatientsModel>> GetAllAsync()
        {
            try
            {
                var patients = await _apiClient.GetAsync<List<PatientsModel>>("Patients/GetAllPatients");
                return patients ?? new List<PatientsModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los pacientes");
                throw;
            }
        }

        public async Task<PatientsModel> GetByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetAsync<PatientsModel>($"Patients/GetPatientById?Id={id}")
                    ?? new PatientsModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener paciente con ID {id}");
                throw;
            }
        }

        public async Task<EditPatientsModel> GetEditModelByIdAsync(int id)
        {
            try
            {
                return await _apiClient.GetAsync<EditPatientsModel>($"Patients/GetPatientById?Id={id}")
                    ?? new EditPatientsModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener modelo de edición para paciente con ID {id}");
                throw;
            }
        }

        public async Task<bool> CreateAsync(CreatePatientsModel model)
        {
            try
            {
                var dto = PatientsFactory.CreateSaveDto(model);

                var result = await _apiClient.PostAsync<SavePatientsDto, OperationResult>(
                    "Patients/SavePatient", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear paciente");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, EditPatientsModel model)
        {
            try
            {
                var dto = PatientsFactory.CreateUpdateDto(model);

                var result = await _apiClient.PutAsync<UpdatePatientsDto, OperationResult>(
                    $"Patients/UpdatePatient/{id}", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar paciente con ID {id}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var dto = new RemovePatientsDto { PatientID = id };
                var result = await _apiClient.DeleteAsync<RemovePatientsDto, OperationResult>(
                    "Patients/DeletePatient", dto);

                return result?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar paciente con ID {id}");
                throw;
            }
        }
    }
}

