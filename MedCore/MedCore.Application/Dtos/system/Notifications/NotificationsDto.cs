
using MedCore.Domain.Entities.users;

namespace MedCore.Application.Dtos.system.Notifications
{
    public class NotificationsDto : DtoBase
    {
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime? SentAt { get; set; }
   
    }
}