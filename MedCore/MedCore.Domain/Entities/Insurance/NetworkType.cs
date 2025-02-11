

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedCore.Domain.Entities.Insurance
{
    public sealed class NetworkType: Base.BaseEntity<int>
    {
        [Column("NetworkTypeId")]
        [Key]
        public override int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
