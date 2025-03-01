
using MedCore.Domain.Entities;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.medical;
using MedCore.Domain.Entities.system;
using Microsoft.EntityFrameworkCore;
using MedCore.Domain.Entities.Insurance;
using MedCore.Domain.Entities.users;

namespace MedCore.Persistence.Context
{
    public class MedCoreContext : DbContext
    {
        public MedCoreContext(DbContextOptions<MedCoreContext> options) : base(options)
        { 
        }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<AvailabilityModes> AvailabilityModes { get; set; }
        public DbSet<MedicalRecords> MedicalRecords { get; set; }
        public DbSet<Specialties> Specialties { get; set; }
        public DbSet<InsuranceProviders> InsuranceProviders { get; set; }
        public DbSet<NetworkType> NetworkType { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Patients> Patients { get; set; }

    }
}
