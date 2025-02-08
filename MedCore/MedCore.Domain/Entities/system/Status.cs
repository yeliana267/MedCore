

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedCore.Domain.Entities.system
{
    internal sealed class Status : Base.BaseEntity<int>
    {
        [Column("StatusID")]
        [Key]
        public override int Id { get; set; }
        public string StatusName { get; set; }
    }
}