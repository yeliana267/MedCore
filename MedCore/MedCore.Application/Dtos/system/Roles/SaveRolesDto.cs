

namespace MedCore.Application.Dtos.system.Roles
{
    public class SaveRolesDto : RolesDto
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
