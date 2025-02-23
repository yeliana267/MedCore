
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.Users;

namespace MedCore.Domain.Base
{
    public abstract class User : BaseEntity<int>
    {
        public object UserID;

        [Column("UserID")]
        [Key]
        public override int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [MaxLength(15)]
        public int PhoneNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; protected set; }
        public short? RoleID { get; set; }




    }
}
