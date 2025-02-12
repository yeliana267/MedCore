
using MedCore.Domain.Entities.system;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.System;

namespace MedCore.Persistence.Repositories.System
{
    public class NotificationRepository : BaseRepository<Notifications, int>, INotificationRepository
    {
        public NotificationRepository(MedCoreContext context) : base(context) 
        { 
       
        }
    }
}
