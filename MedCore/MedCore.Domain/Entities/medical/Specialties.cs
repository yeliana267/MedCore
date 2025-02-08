
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MedCore.Domain.Base;

namespace MedCore.Domain.Entities
{
    public sealed class Specialties : Base.BaseEntity<short>

    {
        [Column("SpecialtyID")]
        [Key]
        public override short Id { get; set; }
        public required string SpecialtyName { get; set; }
    }
    
}
