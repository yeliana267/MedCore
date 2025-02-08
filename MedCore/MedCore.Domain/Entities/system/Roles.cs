using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MedCore.Domain.Entities.system
{
    internal sealed class Roles : Base.BaseEntity<int>
    {
        [Column("Roleid")]
        [Key]

        public override int Id { get; set; }
        public string RoleName { get; set; }
    }
}