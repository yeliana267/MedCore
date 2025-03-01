﻿

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MedCore.Model.Models.Users;

namespace MedCore.Model.Models.appointments
{
    public class AvailabilityModel
    {
        [Key]
        public int AvailabilityID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime AvailableDate { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan EndTime { get; set; }

        [ForeignKey("DoctorID")]
        public virtual DoctorModel Doctor { get; set; }
    }
}
