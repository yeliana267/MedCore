

namespace MedCore.Application.Dtos.users.Users
{
    public class UpdateUsersDto : UsersDto
    {
        public int UserID { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
