﻿

using System.Numerics;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.users;
using MedCore.Domain.Repository;
using MedCore.Persistence.Base;

namespace MedCore.Persistence.Interfaces.users
{
    public interface IDoctorsRepository : IBaseRepository<Doctors, int>
    {
        Task<OperationResult> GetDoctorsBySpecialtyAsync(int specialtyId); // Obtener doctores por especialidad
        Task<OperationResult> UpdateConsultationFeeAsync(int doctorId, decimal consultationFee); // Actualizar tarifa de consulta
        Task<OperationResult> GetDoctorsWithExpiringLicenseAsync(DateTime expirationDate); // Obtener doctores con licencia próxima a expirar
    }
}
