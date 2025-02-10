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

        Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter);

        Task<OperationResult> GetEntityByAsync(int Id);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
    }
}
