

using MedCore.Application.Base;
using MedCore.Application.Dtos.system.Roles;

namespace MedCore.Application.Interfaces.system
{
    public interface IRolesService : IBaseService<SaveRolesDto, UpdateRolesDto, RemoveRolesDto> 
    {
    }
}
