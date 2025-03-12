

using MedCore.Application.Base;
using MedCore.Application.Dtos.appointments.Appointments;

namespace MedCore.Application.Interfaces.appointments
{
    public interface IAppointmentsService : IBaseService<SaveAppointmentsDto, UpdateAppointmentsDto,RemoveAppointmentsDto>
    {

    }
}
