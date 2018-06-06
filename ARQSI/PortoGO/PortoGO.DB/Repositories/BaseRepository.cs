using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Repositories
{
    public abstract class BaseRepository<TEntity, TId> :
                IBaseRepository<TEntity, TId> where TEntity : class
    {

        internal readonly DbContext Context;


        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity, TId}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BaseRepository(DbContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Gets the number of all entities in this repository
        /// </summary>
        /// <returns>
        /// The number of entities
        /// </returns>
        public int Count()
        {
            return this.Context.Set<TEntity>().Count();
        }

        /// <summary>
        /// Gets the number of entities that match the <paramref name="predicate" />
        /// </summary>
        /// <param name="predicate">A condition to count entities</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Context.Set<TEntity>().Where(predicate).Count();
        }

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">Primary key of the entity to delete</param>
        public void Delete(TId id)
        {
            TEntity entityToDelete = this.Context.Set<TEntity>().Find(id);

            this.Delete(entityToDelete);
        }

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        public void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                this.Context.Set<TEntity>().Attach(entity);
            }

            this.Context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Gets a list of entities for the given predicate
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>
        /// Query result
        /// </returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Context.Set<TEntity>().Where(predicate).ToList();
        }

        /// <summary>
        /// Gets an ordered list of entities for the given predicate
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <param name="orderBy">A condition to order entities</param>
        /// <param name="includeProperties">Coma-delimited list of properties to eager load</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.Context.Set<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Gets an entity for the given predicate or null if not found
        /// <see cref="M:System.Linq.Enumerable.FirstOrDefault``1(System.Collections.Generic.IEnumerable{``0})" />
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        /// <summary>
        /// Gets an entity with a given primary key
        /// </summary>
        /// <param name="id">Primary key of the entity to select</param>
        /// <returns>
        ///   <see cref="!:TEntity" />The entity
        /// </returns>
        public virtual TEntity Get(TId id)
        {
            return this.Context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns>
        /// Query result
        /// </returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.Context.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="includeProperties">The navigation properties to include.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll(string includeProperties = "")
        {
            IQueryable<TEntity> query = this.Context.Set<TEntity>();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }

        /// <summary>
        /// Inserts a new entity
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity)
        {
            this.Context.Set<TEntity>().Add(entity);

            return entity;
        }

        /// <summary>
        /// Inserts or updates an entity depending on the Id value
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity InsertOrUpdate(TEntity entity)
        {
            this.Context.Set<TEntity>().AddOrUpdate(entity);

            return entity;
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        public void Update(TEntity entity)
        {
            if (this.Context.Entry(entity).State == EntityState.Detached)
            {
                this.Context.Set<TEntity>().Attach(entity);
            }

            this.Context.Entry(entity).State = EntityState.Modified;
        }

    }
}
