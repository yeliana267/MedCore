using MedCore.Web.Models.appointments;

namespace MedCore.Web.Interfaces.appointments.appointment
{
    public interface IAppointmentWeb
    {
        Task<List<AppointmentModel>> GetAllAsync();
        Task<AppointmentModel> GetByIdAsync(int id);
        Task<EditAppointmentModel> GetEditModelByIdAsync(int id);
        Task<bool> CreateAsync(CreateAppointmentModel model);
        Task<bool> UpdateAsync(int id, EditAppointmentModel model);
        Task<bool> DeleteAsync(int id);
    }
}
