﻿

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Repository;
using System.Buffers;
using System.Net.NetworkInformation;
using System.Numerics;

namespace MedCore.Persistence.Interfaces.appointments
{
    public interface IAppointmentsRepository : IBaseRepository<Appointments, int>
    {
        Task<OperationResult> GetAppointmentsByDoctorIdAsync(int doctorId); //Obtiene todas las citas de un doctor específico

        Task<OperationResult> GetAppointmentsByPatientIdAsync(int patientId); //todas las citas de un paciente específico
        Task<OperationResult> GetPendingAppointmentsAsync(); //Obtener citas pendientes


    }
}
