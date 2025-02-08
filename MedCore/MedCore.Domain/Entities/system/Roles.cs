using MedCore.Domain.Base;

namespace MedCore.Domain.Entities.system
{
    internal sealed class Roles : BaseEntity
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
