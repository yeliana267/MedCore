

using MedCore.Domain.Base;

namespace MedCore.Application.Base
{
    public interface IBaseService<TDtoSave, TDtoUpdate,TDoRemove>
    {
        Task<OperationResult>GetAll();
        Task<OperationResult> GetById(int Id);
        Task<OperationResult> Save(TDtoSave dto);
        Task<OperationResult> Update(TDtoUpdate dto);
        Task<OperationResult> Remove(TDoRemove dto);

    }
}
