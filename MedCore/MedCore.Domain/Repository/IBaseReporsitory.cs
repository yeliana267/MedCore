﻿using MedCore.Domain.Base;
using System.Linq.Expressions;


namespace MedCore.Domain.Repository
{
    public interface IBaseRepository<TEntity, TType> where TEntity : class 
    {
        Task<TEntity> GetEntityByIdAsync(TType id); 
        Task<OperationResult> UpdateEntityAsync(TType id, TEntity entity);

        Task<OperationResult> SaveEntityAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();

        Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);

        Task<OperationResult> DeleteEntityByIdAsync(TType id);
    }
}
