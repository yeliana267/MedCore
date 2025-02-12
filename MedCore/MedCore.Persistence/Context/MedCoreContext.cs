

using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.system;
using Microsoft.EntityFrameworkCore;

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
    }
}
