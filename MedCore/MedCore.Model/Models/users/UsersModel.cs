

using System.ComponentModel.DataAnnotations;

namespace MedCore.Model.Models.users
{
    public class UsersModel
        {
            [Key]
            public int UserID { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
            public int? RoleID { get; set; }


    }
}
