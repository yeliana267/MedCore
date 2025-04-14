using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Web.Models.appointments;

namespace MedCore.Web.Repositories.appointmentsRepository.appointment
{
    public class AppointmentsFactory
    {
        public static SaveAppointmentsDto CreateSaveDto(CreateAppointmentModel model)
        => new()
        {
            PatientID = model.PatientID,
            DoctorID = model.DoctorID,
            AppointmentDate = model.AppointmentDate,
            StatusID = model.StatusID,
            CreatedAt = DateTime.UtcNow
        };

        public static UpdateAppointmentsDto CreateUpdateDto(EditAppointmentModel model)
            => new()
            {
                AppointmentID = model.AppointmentID,
                PatientID = model.PatientID,
                DoctorID = model.DoctorID,
                AppointmentDate = model.AppointmentDate,
                StatusID = model.StatusID,
                UpdatedAt = DateTime.UtcNow
            };
    }
}
