using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Repositories
{
    public interface IBaseRepository<TEntity, TId> where TEntity : class
    {
        #region Query/Select

        /// <summary>
        /// Gets an entity with a given primary key
        /// </summary>
        /// <param name="id">Primary key of the entity to select</param>
        /// <returns><see  cref="TEntity"/>The entity</returns>
        TEntity Get(TId id);

        /// <summary>
        /// Gets an entity for the given predicate or null if not found
        /// <see cref="Enumerable.FirstOrDefault{TSource}(IEnumerable{TSource})"/> 
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns>Query result</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="includeProperties">The navigatio properties to include.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(string includeProperties = "");

        /// <summary>
        /// Gets a list of entities for the given predicate
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>Query result</returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an ordered list of entities for the given predicate
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <param name="orderBy">A condition to order entities</param>
        /// <param name="includeProperties">Coma-delimited list of properties to eager load</param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        #endregion

        #region Aggregates

        /// <summary>
        /// Gets the number of all entities in this repository
        /// </summary>
        /// <returns>The number of entities</returns>
        int Count();

        /// <summary>
        /// Gets the number of entities that match the <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">A condition to count entities</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">Primary key of the entity to delete</param>
        void Delete(TId id);

        #endregion

        #region Insert

        /// <summary>
        /// Inserts a new entity
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <returns></returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts or updates an entity depending on the Id value
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity InsertOrUpdate(TEntity entity);

        #endregion

        #region Update

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns></returns>
        //TEntity Update(TEntity entity);
        void Update(TEntity entity);

        #endregion

    }
}
