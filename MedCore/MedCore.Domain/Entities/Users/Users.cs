
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MedCore.Domain.Base;

namespace MedCore.Domain.Entities.Users
{
    public class Users<Ttype> : BaseEntity<int>
    {
        [Column("UserID")]
        [Key]
        public override int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public short? RoleID { get; set; }

    }
}
