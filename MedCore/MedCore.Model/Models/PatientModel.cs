

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedCore.Model.Models
{
    public class PatientModel
    {
        [Column("PatientID")]
        [Key]
        public int IPatientIDd { get; set; }
        [Required]
        public int InsuranceProviderID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
