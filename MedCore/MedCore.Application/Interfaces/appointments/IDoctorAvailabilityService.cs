
using MedCore.Application.Base;
using MedCore.Application.Dtos.appointments.DoctorAvailability;

namespace MedCore.Application.Interfaces.appointments
{
    public interface IDoctorAvailabilityService : IBaseService<SaveDoctorAvailabilityDto, UpdateDoctorAvailabilityDto, RemoveDoctorAvailabilityDto>
    {
    }
}
