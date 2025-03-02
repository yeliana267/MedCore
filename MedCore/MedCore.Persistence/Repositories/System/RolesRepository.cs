
using System.Data;
using MedCore.Domain.Entities.system;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.System;
using Microsoft.EntityFrameworkCore;

namespace MedCore.Persistence.Repositories.System
{
    public class RolesRepository : BaseRepository<Roles, int>, IRolesRepository
    {
        public RolesRepository(MedCoreContext context) : base(context)
        {
        }

        public async Task<bool> AddRoleAsync(Roles role)
        {
            bool roleExists = await _context.Roles
                .AnyAsync(r => r.RoleName == role.RoleName);

            if (roleExists)
            {
                throw new InvalidOperationException("El rol con este nombre ya existe.");
            }

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
