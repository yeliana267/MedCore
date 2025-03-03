
using MedCore.Application.Base;
using MedCore.Application.Dtos.system.Status;

namespace MedCore.Application.Interfaces.system
{
    public interface IStatusService : IBaseService<SaveStatusDto, UpdateStatusDto, RemoveStatusDto>
    {
    }
}
