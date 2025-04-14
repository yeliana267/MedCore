using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Application.Dtos.appointments.DoctorAvailability;
using MedCore.Web.Models.appointments;
using MedCore.Web.Models.doctorAvailability;

namespace MedCore.Web.Repositories.appointmentsRepository.doctorAvailability
{
    public class DoctorAvailabilityFactory
    {
   
            public static SaveDoctorAvailabilityDto CreateSaveDto(CreateDoctorAvailabilityModel model)
            => new()
            {

                DoctorID = model.DoctorID,
                AvailableDate = model.AvailableDate,
                StartTime = model.StartTime,
                EndTime = model.EndTime
    };

            public static UpdateDoctorAvailabilityDto CreateUpdateDto(EditDoctorAvailabilityModel model)
                => new()
                {
                           
                    AvailabilityID = model.AvailabilityID,
                    DoctorID = model.DoctorID,
                    AvailableDate = model.AvailableDate,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime
    };
        }
    }

