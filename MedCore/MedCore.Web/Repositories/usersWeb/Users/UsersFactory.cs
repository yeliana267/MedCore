using MedCore.Application.Dtos.users.Users;
using MedCore.Web.Models.users.users;

namespace MedCore.Web.Repositories.usersWeb.Users
{
    public static class UsersFactory
    {
        public static SaveUsersDto CreateSaveDto(CreateUsersModel model)
        => new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Password = model.Password,
            RoleID = model.RoleID,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        public static UpdateUsersDto CreateUpdateDto(EditUsersModel model)
            => new()
            {
                UserID = model.UserID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                RoleID = model.RoleID,
                IsActive = model.IsActive,
                UpdatedAt = DateTime.UtcNow
            };
    }
}
