﻿using System;
using Application.DataAccess.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.DataAccess.Abstract.Repository
{
    /// <summary>
    /// Generic repository pattern interface.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Unique key of entity. It should be int, Guid etc.</typeparam>
    public interface IRepository<TEntity, in TKey> where TEntity : BaseEntity<TKey>
    {
        /// <summary>
        /// Get TEntity by id value.
        /// </summary>
        /// <param name="id">Unique id value.</param>
        /// <returns></returns>
        TEntity GetById(TKey id);

        /// <summary>
        /// Get TEntity list.
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetList();

        /// <summary>
        /// Insert new TEntity.
        /// </summary>
        /// <param name="entity">TEntity value.</param>
        /// <returns></returns>
        bool Add(TEntity entity);
        
        bool AddRange(List<TEntity> entities);

        /// <summary>
        /// Update TEntity.
        /// </summary>
        /// <param name="entity">TEntity value.</param>
        /// <returns></returns>
        bool Update(TEntity entity);

        /// <summary>
        /// Remove TEntity.
        /// </summary>
        /// <param name="entity">TEntity value.</param>
        /// <returns></returns>
        bool Remove(TEntity entity);

        /// <summary>
        /// Get all item count.
        /// </summary>
        /// <returns></returns>
        int GetCount();

        /// <summary>
        /// Get TEntity by id value.
        /// </summary>
        /// <param name="id">Unique id value.</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(TKey id);

        /// <summary>
        /// Get TEntity list.
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync();

        /// <summary>
        /// Insert new TEntity.
        /// </summary>
        /// <param name="entity">TEntity value.</param>
        /// <returns></returns>
        Task<bool> AddAsync(TEntity entity);

        /// <summary>
        /// Update TEntity.
        /// </summary>
        /// <param name="entity">TEntity value.</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Remove TEntity.
        /// </summary>
        /// <param name="entity">TEntity value.</param>
        /// <returns></returns>
        Task<bool> RemoveAsync(TEntity entity);

        Task<bool> RemoveRangeAsync(List<TEntity> entities);

        Task<bool> RemoveAsyncById(TKey id);

        /// <summary>
        /// Get all item count.
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// item soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool SoftRemove(TKey id);

        /// <summary>
        /// item soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SoftRemoveAsync(TKey id);
        
        TEntity Find(Expression<Func<TEntity, bool>> expression);

        IList<TEntity> FindAll(Expression<Func<TEntity, bool>> expression);

        IQueryable GetQueryable(Expression<Func<TEntity, bool>> expression);
    }
}
