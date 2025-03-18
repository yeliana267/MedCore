
using MedCore.Domain.Entities.system;
using MedCore.Domain.Repository;

namespace MedCore.Persistence.Interfaces.System
{
    public interface IStatusRepository : IBaseRepository<Status, int>
    {
    }
}
