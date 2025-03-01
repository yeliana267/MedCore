

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedCore.Domain.Entities.users
{
    [Table("Users", Schema = "users")]
  
    public sealed class Users : Base.BaseEntity<int>
    {
        [Column("UserID")]
        [Key]
        public override int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleID { get; set; }

    }
}
