

using MedCore.Application.Dtos.users.Doctors;
using MedCore.Application.Interfaces;
using MedCore.Domain.Base;
using MedCore.Persistence.Interfaces.System;
using MedCore.Persistence.Interfaces.users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly IDoctorsRepository _doctorsRepository ;
        private readonly ILogger<DoctorsService> _logger;
        private readonly IConfiguration _configuration;

        public DoctorsService(IDoctorsRepository doctorsRepository, ILogger<DoctorsService> logger, IConfiguration configuration)
        {
            _doctorsRepository = doctorsRepository;
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

        public Task<OperationResult> Remove(RemoveDoctorsDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveDoctorsDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateDoctorsDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
