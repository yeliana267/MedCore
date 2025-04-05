using System.ComponentModel.DataAnnotations;

namespace MedCore.Web.Models.users.User
{
    public class UsersModel
    {
        public int Id { get; set; }
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
