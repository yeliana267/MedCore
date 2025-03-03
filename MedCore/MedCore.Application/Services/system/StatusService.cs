
using MedCore.Application.Dtos.system.Status;
using MedCore.Application.Interfaces.system;
using MedCore.Domain.Base;
using MedCore.Persistence.Interfaces.System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MedCore.Application.Services.system
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly ILogger<StatusService> _logger;
        private readonly IConfiguration _configuration;
        public StatusService(IStatusRepository statusRepository, 
            ILogger<StatusService> logger,
            IConfiguration configuration) {
            _statusRepository = statusRepository;
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

        public Task<OperationResult> Remove(RemoveStatusDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveStatusDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateStatusDto dto)
        {
            throw new NotImplementedException();
        }
    }

}
