

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Repository;
using MedCore.Model.Models.appointments;
using MedCore.Persistence.Repositories.appointments;
using System.Buffers;
using System.Net.NetworkInformation;
using System.Numerics;

namespace MedCore.Persistence.Interfaces.appointments
{
    public interface IAppointmentsRepository : IBaseReporsitory<Appointments, int>
    {
        Task<OperationResult> GetAppointmentsByDoctorIdAsync(int doctorId); //Obtiene todas las citas de un doctor específico

        Task<OperationResult> GetAppointmentsByPatientIdAsync(int patientId); //todas las citas de un paciente específico
        Task<OperationResult> GetPendingAppointmentsAsync(); //Obtener citas pendientes
    }
}
