using PortoGO.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PortoGO.DB.Tests.Mocks.Repositories
{
    public abstract class BaseRepositoryMock<TEntity, TId> :
                IBaseRepository<TEntity, TId> where TEntity : class
    {
        protected IQueryable<TEntity> mockData;

        public abstract void Setup(); 

        public int Count()
        {
            return this.mockData.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.mockData.Count();
        }

        public void Delete(TId id)
        {
            TEntity location = this.Get(id);

            this.Delete(location);
        }

        public void Delete(TEntity entity)
        {
            var data = this.mockData.ToList();

            data.Remove(entity);

            this.mockData = data.AsQueryable();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.mockData.Where(predicate);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return this.mockData.Where(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return this.mockData.Where(predicate).FirstOrDefault();
        }

        public abstract TEntity Get(TId id);

        public IEnumerable<TEntity> GetAll()
        {
            return this.mockData;
        }

        public IEnumerable<TEntity> GetAll(string includeProperties = "")
        {
            return this.mockData;
        }

        public abstract TEntity Insert(TEntity entity);

        public TEntity InsertOrUpdate(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public abstract void Update(TEntity entity);
    }
}
