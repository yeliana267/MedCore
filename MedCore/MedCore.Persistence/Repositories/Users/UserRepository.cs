

using MedCore.Domain.Base;
using MedCore.Model.Models.Users;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedCore.Persistence.Repositories.Users
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        private readonly MedCoreContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly IConfiguration _configuration;

        public UserRepository(MedCoreContext context, 
                              ILogger<UserRepository> logger, 
                              IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            //OperationResult result = new OperationResult();
            //try
            //{
            //    var querys = await (from user in _context.Users
            //                        where user.Email == email
            //                        select new UserModel{
            //                            UserID = user.Id,
            //                            Email = user.Email,
            //                            FirstName = user.FirstName,
            //                        }).ToListasync();
            //}
            throw new NotImplementedException();
        }

        public async Task<User?> GetByEmailConfirmationTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByPasswordResetTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> UpdateConfirmationTokenAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> UpdateResetPasswordTokenAsync(User user)
        {
            throw new NotImplementedException();
        }

        public override Task<OperationResult> SaveEntityAsync(User entity)
        {
            //Agregar validacciones

            return base.SaveEntityAsync(entity);
        }

        public async Task<OperationResult> UpdateContactInfoAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
