
using MedCore.Application.Dtos.system.Notifications;
using MedCore.Application.Interfaces.system;
using MedCore.Domain.Base;
using MedCore.Persistence.Interfaces.System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.system
{
    public class NotificationsService : INotificationsService
    {
        private readonly INotificationsRepository _notificationRepository;
        private readonly ILogger<NotificationsService> _logger;
        private readonly IConfiguration _configuration;
        public NotificationsService(INotificationsRepository notificationRepository, 
            ILogger <NotificationsService> logger,
            IConfiguration configuration) {
            _notificationRepository = notificationRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public Task<OperationResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemoveNotificationsDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveNotificationsDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateNotificationsDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
