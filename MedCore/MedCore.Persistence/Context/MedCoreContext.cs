

using System.Numerics;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.Users;
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
        public DbSet<UsersDoctors> UsersDoctors { get; set; }
        public DbSet<UsersPatients> UsersPatients { get; set; }
    }
}
