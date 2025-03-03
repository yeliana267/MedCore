
using MedCore.Application.Dtos.system.Roles;
using MedCore.Application.Interfaces.system;
using MedCore.Domain.Base;
using MedCore.Persistence.Interfaces.System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services.system
{
      public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public RolesService(IRolesRepository rolesRepository,
            ILogger<RolesService> logger,
            IConfiguration configuration) {

            _rolesRepository = rolesRepository;
            _configuration = configuration;
            _logger = logger;
        }
        public Task<OperationResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemoveRolesDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveRolesDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateRolesDto dto)
        {
            throw new NotImplementedException();
        }
    }

}