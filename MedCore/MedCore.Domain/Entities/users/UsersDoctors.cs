﻿

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MedCore.Domain.Base;

namespace MedCore.Domain.Entities.Users
{
    public sealed class UsersDoctors : Users
    {
        [Column("DoctorID")]
        [Key]
        public override int Id { get; set; }
        public short SpecialtyID { get; set; }
        public string LicenseNumber { get; set; }
        public int PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string Education { get; set; }
        public string? Bio { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public short? AvailabilityModeId { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        
    }
}
