
using MedCore.Application.Dtos.users.Users;
using MedCore.Application.Interfaces;
using MedCore.Domain.Base;
using MedCore.Persistence.Interfaces.users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<UsersService> _logger;
        private readonly IConfiguration _configuration;

        public UsersService(IUsersRepository usersRepository, ILogger<UsersService> logger, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
               var users = await _usersRepository.GetAllAsync();

                result.Data = users;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
            }

            return result;
        }

        public Task<OperationResult> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemoveUsersDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveUsersDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateUsersDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
