using MedCore.Domain.Entities.system;
using MedCore.Persistence.Base;
using MedCore.Persistence.Context;
using MedCore.Persistence.Interfaces.System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MedCore.Persistence.Repositories.System
{
    public class StatusRepository : BaseRepository<Status, int>, IStatusRepository
    {
        public StatusRepository(MedCoreContext context) : base(context)
        {
        }

        public async Task<bool> AddStatusAsync(Status status)
        {
            bool statusExists = await _context.Status
                .AnyAsync(s => s.StatusName == status.StatusName);

            if (statusExists)
            {
                throw new InvalidOperationException("El estado con este nombre ya existe.");
            }

            _context.Status.Add(status);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
