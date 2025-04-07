using MedCore.Application.Dtos.users.Doctors;
using MedCore.Domain.Base;
using MedCore.Web.Interfaces;
using MedCore.Web.Interfaces.users;
using MedCore.Web.Models.users.Doctors;

namespace MedCore.Web.Repositories.usersWeb.Doctors
{
    public class DoctorsWeb
    {
        public class DoctorWeb : IDoctorsWeb
        {
            private readonly IApiClient _apiClient;
            private readonly ILogger<DoctorWeb> _logger;

            public DoctorWeb(IApiClient apiClient, ILogger<DoctorWeb> logger)
            {
                _apiClient = apiClient;
                _logger = logger;
            }

            public async Task<List<DoctorsModel>> GetAllAsync()
            {
                try
                {
                    var doctors = await _apiClient.GetAsync<List<DoctorsModel>>("Doctors/GetAllDoctors");
                    return doctors ?? new List<DoctorsModel>();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener todos los doctores");
                    throw;
                }
            }

            public async Task<DoctorsModel> GetByIdAsync(int id)
            {
                try
                {
                    return await _apiClient.GetAsync<DoctorsModel>($"Doctors/GetDoctorById?Id={id}")
                        ?? new DoctorsModel();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al obtener doctor con ID {id}");
                    throw;
                }
            }

            public async Task<EditDoctorsModel> GetEditModelByIdAsync(int id)
            {
                try
                {
                    return await _apiClient.GetAsync<EditDoctorsModel>($"Doctors/GetDoctorById?Id={id}")
                        ?? new EditDoctorsModel();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al obtener modelo de edición para doctor con ID {id}");
                    throw;
                }
            }

            public async Task<bool> CreateAsync(CreateDoctorsModel model)
            {
                try
                {
                    var dto = DoctorsFactory.CreateSaveDto(model);

                    var result = await _apiClient.PostAsync<SaveDoctorsDto, OperationResult>(
                        "Doctors/SaveDoctor", dto);

                    return result?.Success ?? false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear doctor");
                    throw;
                }
            }

            public async Task<bool> UpdateAsync(int id, EditDoctorsModel model)
            {
                try
                {
                    var dto = DoctorsFactory.CreateUpdateDto(model);

                    var result = await _apiClient.PutAsync<UpdateDoctorsDto, OperationResult>(
                        $"Doctors/UpdateDoctor/{id}", dto);

                    return result?.Success ?? false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al actualizar doctor con ID {id}");
                    throw;
                }
            }

            public async Task<bool> DeleteAsync(int id)
            {
                try
                {
                    var dto = new RemoveDoctorsDto { DoctorID = id };
                    var result = await _apiClient.DeleteAsync<RemoveDoctorsDto, OperationResult>(
                        "Doctors/DeleteDoctor", dto);

                    return result?.Success ?? false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al eliminar doctor con ID {id}");
                    throw;
                }
            }
        }
    }
}
