

using MedCore.Domain.Entities.appointments;
using Microsoft.EntityFrameworkCore;

namespace MedCore.Persistence.Context
{
    public class MedCoreContext : DbContext
    {
        public MedCoreContext(DbContextOptions<MedCoreContext> options) : base(options)
        { 
        }
       
    }
}
