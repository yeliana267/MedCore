

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedCore.Domain.Entities.Insurance
{
    internal sealed class NetworkType: Base.BaseEntity<int>
    {
        [Column("NetworkTypeId")]
        [Key]
        public override int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
