
using MedCore.Domain.Base;
using MedCore.Domain.Repository;
using MedCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace MedCore.Persistence.Base
{
    public abstract class BaseRepository<TEntity, TType> : IBaseReporsitory<TEntity, TType> where TEntity : class 
    {
        private readonly MedCoreContext _context;
        private DbSet<TEntity> Entity { get; set; }

        protected BaseRepository(MedCoreContext context)
        {
            _context = context;
            this.Entity = _context.Set<TEntity>();
        }
        public virtual async Task<TEntity> GetEntityByIdAsync(TType id)
        {
            return await Entity.FindAsync(id);
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TType id, TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                Entity.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} actualizando la entidad.";

            }

            return result;
        }
        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                Entity.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} guardando la entidad.";

            }

            return result;
        }
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Entity.ToListAsync();
        }

        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                var datos = await this.Entity.Where(filter).ToListAsync();
                result.Data = datos;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} obteniendo los datos.";
            }

            return result;

        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Entity.AnyAsync(filter);
        }

    }
}
