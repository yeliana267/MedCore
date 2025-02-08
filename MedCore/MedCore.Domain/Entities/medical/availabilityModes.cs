using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MedCore.Domain.Entities.medical

{
    public sealed class AvailabilityModes : Base.BaseEntity<short>
    {
        [Column("SAvailabilityModelID")]
        [Key]
        public override short Id { get; set; }
        public required string AvailabilityMode { get; set; }
       
    }
}
