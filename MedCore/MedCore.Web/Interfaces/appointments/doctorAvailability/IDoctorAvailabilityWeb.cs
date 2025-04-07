using MedCore.Web.Models.appointments;
using MedCore.Web.Models.doctorAvailability;

namespace MedCore.Web.Interfaces.appointments.doctorAvailability
{
    public interface IDoctorAvailabilityWeb
    {
        Task<List<DoctorAvailabilityModel>> GetAllAsync();
        Task<DoctorAvailabilityModel> GetByIdAsync(int id);
        Task<EditDoctorAvailabilityModel> GetEditModelByIdAsync(int id);
        Task<bool> CreateAsync(CreateDoctorAvailabilityModel model);
        Task<bool> UpdateAsync(int id, EditDoctorAvailabilityModel model);
        Task<bool> DeleteAsync(int id);
    }
}
