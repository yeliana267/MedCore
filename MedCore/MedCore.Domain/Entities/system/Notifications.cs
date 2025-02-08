using MedCore.Domain.Base;

namespace MedCore.Domain.Entities.system
{
    public sealed class Notifications : BaseEntity
    {
        public int IdNotifications { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
