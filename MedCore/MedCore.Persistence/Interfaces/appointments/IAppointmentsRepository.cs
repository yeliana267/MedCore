

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Repository;
using System.Buffers;
using System.Net.NetworkInformation;
using System.Numerics;

namespace MedCore.Persistence.Interfaces.appointments
{
    public interface IAppointmentsRepository : IBaseReporsitory<Appointments, int>
    {
        Task<OperationResult> GetAppointmentsByDoctorIdAsync(int doctorId); //Obtiene todas las citas de un doctor específico
        Task<OperationResult> GetAppointmentsByPatientIdAsync(int patientId); //todas las citas de un paciente específico
        Task<OperationResult> GetAppointmentsByDateAsync(DateTime date); //Filtra las citas por una fecha en específico
        Task<OperationResult> CancelAppointmentAsync(int appointmentId); // Cancelar una cita
        Task<OperationResult> ConfirmAppointmentAsync(int appointmentId);
        Task<OperationResult> GetPendingAppointmentsAsync(); //Obtener citas pendientes
        Task<OperationResult> GetConfirmedAppointmentsAsync();// Obtener citas en estado "Cancelado"
        Task<OperationResult> GetCancelledAppointmentsAsync();


    }
}
