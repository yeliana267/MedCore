

namespace MedCore.Application.Dtos.users.Users
{
    public class SaveUsersDto : UsersDto
    { 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
