using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedCore.Domain.Entities.system;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.System
{
    public interface INotificationRepository : IBaseReporsitory<Notifications, int>

    {
    }
}
