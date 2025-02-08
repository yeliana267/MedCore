using MedCore.Domain.Base;
using System.Linq.Expressions;


namespace MedCore.Domain.Repository
{
    public interface IBaseReporsitory <TEntity, TType> where TEntity : class 
    {
        Task<TEntity> GetEntityByIdAsync(TType id); 
Task UpdateEntityAsync(TEntity entity);

Task DeleteEntityAsync(TEntity entity);
        Task SaveEntityAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();

        Task<OperationResult> GetAll(Expression<Func<TEntity, bool>> filter);

        Task<OperationResult> GetEntityBy(int Id);

        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);
    }
}
