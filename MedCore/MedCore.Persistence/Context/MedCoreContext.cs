﻿

using System.Numerics;
using MedCore.Domain.Entities.Users;
using MedCore.Domain.Base;
using MedCore.Domain.Entities;
using MedCore.Domain.Entities.appointments;
using MedCore.Domain.Entities.medical;
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
        public DbSet<UsersDoctors> UsersDoctors { get; set; }
        public DbSet<UsersPatients> UsersPatients { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<AvailabilityModes> AvailabilityModes { get; set; }
        public DbSet<MedicalRecords> MedicalRecords { get; set; }
        public DbSet<Specialties> Specialties { get; set; }

    }
}
