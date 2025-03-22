﻿

namespace MedCore.Application.Dtos.users.Users
{
    public class UsersDto : DtoBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
    }
}
