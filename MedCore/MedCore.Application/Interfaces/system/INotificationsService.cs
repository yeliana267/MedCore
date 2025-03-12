
using MedCore.Application.Base;
using MedCore.Application.Dtos.system.Notifications;

namespace MedCore.Application.Interfaces.system
{
    public interface INotificationsService : IBaseService<SaveNotificationsDto, UpdateNotificationsDto, RemoveNotificationsDto>
    {
    }
}
