

using MedCore.Domain.Base;
using MedCore.Domain.Entities.Users;
using MedCore.Model.Models;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.Users
{
    public class DoctorRepository : BaseRepository<Patient, int>, IDoctorRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<DoctorRepository> _logger;
        private readonly IConfiguration _configuration;

        public DoctorRepository(MedCoreContext context,
                              ILogger<DoctorRepository> logger,
                              IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        //public async Task<List<Doctor>> GetDoctorsBySpecialtyAsync(short specialtyId)
        //{
        //    OperationResult result = new OperationResult();
        //    try
        //    {
        //        var querys = await (from doctor in _context.Doctors
        //                            join specialty in _context.Specialties on doctor.SpecialtyID equals specialty.SpecialtyID
        //                            join user in _context.Users on doctor.DoctorID equals user.Id
        //                            where doctor.IsActive == true
        //                            orderby specialty.SpecialtyName, user.LastName
        //                            select new DoctorModel()
        //                            {
        //                                FirstName = user.FirstName,
        //                                LastName = user.LastName,
        //                                SpecialtyName = specialty.SpecialtyName,
        //                                LicenseNumber = doctor.LicenseNumber
        //                            }).ToListAsync();

        //        result.Data = querys;
        //        result.Success = true;
        //        result.Message = "Citas obtenidas exitosamente.";

        //    }
        //    catch (Exception ex)
        //    {
        //      result.Message =  _configuration["ErrorAppointmentsRepository:GetAppointmentsByDoctor"]
        //                                        ?? "Error desconocido al obtener citas del doctor.";
        //        result.Success = false;
        //        this._logger.LogError(result.Message, ex.ToString());
        //    }

        //    return result;
        //}

    
 
        public async Task<OperationResult> UpdateDoctorInfoAsync(Doctor doctor)
        {
            OperationResult result = new OperationResult();

            return result;
        }
        

        public async Task<OperationResult> UpdateDoctorProfileAsync(Doctor doctor)
        {
            OperationResult result = new OperationResult();

            return result;

        }
    }
    }

