
using MedCore.Domain.Base;
using MedCore.Domain.Entities;
using MedCore.Domain.Entities.medical;
using MedCore.Domain.Repository;
using MedCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace MedCore.Persistence.Base
{
    public abstract class BaseRepository<TEntity, TType> : IBaseRepository<TEntity, TType> where TEntity : class 
    {
        public readonly MedCoreContext _context;
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

        //public virtual async Task<OperationResult> UpdateEntityAsync(TType id, TEntity entity)
        //{
        //    OperationResult result = new OperationResult();

        //    try
        //    {
        //        Entity.Update(entity);
        //        await _context.SaveChangesAsync();
        //        result.Success = true;
        //            result.Message = "Entidad actualizada correctamente.";
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.Message = $"Ocurrió un error {ex.Message} actualizando la entidad.";

        //    }

        //    return result;
        //}


        public virtual async Task<OperationResult> UpdateEntityAsync(TType id, TEntity entity)
        {
            var result = new OperationResult();

            try
            {
                var existingEntity = await Entity.FindAsync(id);

                if (existingEntity == null)
                {
                    result.Success = false;
                    result.Message = "Entidad no encontrada.";
                    return result;
                }

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);

                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = "Entidad actualizada correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error actualizando la entidad: {ex.Message}";
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
                result.Success = true;
                result.Message = "Entidad guardada correctamente.";
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
        public virtual async Task<OperationResult> DeleteEntityByIdAsync(TType id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var entity = await Entity.FindAsync(id);
                Entity.Remove(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Entidad eliminada correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrió un error {ex.Message} eliminando la entidad.";
            }
            return result;
        }

   
    }
}

