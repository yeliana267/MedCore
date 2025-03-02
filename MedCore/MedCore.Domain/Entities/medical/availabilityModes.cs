using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MedCore.Domain.Entities.medical

{
    [Table("AvailabilityModes", Schema = "medical")]
    public sealed class AvailabilityModes : Base.BaseEntity<short>
    {
        [Column("SAvailabilityModeID")]
        [Key]
        public override short Id { get; set; }
        public required string AvailabilityMode { get; set; }

    }
}
