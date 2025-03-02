
namespace MedCore.Application.Dtos.system.Roles
{
    public class UpdateRolesDto : RolesDto
    {
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
        