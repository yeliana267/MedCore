
using MedCore.Domain.Entities.system;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.System;
using Microsoft.EntityFrameworkCore;

namespace MedCore.Persistence.Repositories.System
{
    public class NotificationsRepository : BaseRepository<Notifications, int>, INotificationsRepository
    {
        public NotificationsRepository(MedCoreContext context) : base(context)
        {

        }

        public async Task<bool> SendNotificationAsync(Notifications notification)
        {

            //validar que no hayan valores nulos
            if (string.IsNullOrWhiteSpace(notification.Message))
            {
                throw new ArgumentException("El mensaje de la notificación no puede estar vacío.");
            }

            // Validar si el usuario ya tiene una notificación con el mismo mensaje
            bool existsDuplicate = await _context.Set<Notifications>()
                .AnyAsync(n => n.UserId == notification.UserId &&
                               n.Message == notification.Message);

            if (existsDuplicate)
            {
                throw new InvalidOperationException("El usuario ya tiene una notificación con este mensaje.");
            }

            // Agregar y guardar la nueva notificación
            await _context.Set<Notifications>().AddAsync(notification);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
