using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MedCore.Domain.Entities.medical
{
    [Table("MedicalRecords", Schema = "medical")]
    public sealed class MedicalRecords : Base.BaseEntity<int>
    {
        [Column("RecordID")]
        [Key]
        public override int Id { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public required string Diagnosis { get; set; }
        public required string Treatment { get; set; }
        public DateTime DateOfVisit { get; set; }
    }
    
}
