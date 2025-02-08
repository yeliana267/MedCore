using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MedCore.Domain.Entities.system
{
    public sealed class Notifications : Base.BaseEntity<int>
    {
        [Column("IdNotifications")]
        [Key]
        public override int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
