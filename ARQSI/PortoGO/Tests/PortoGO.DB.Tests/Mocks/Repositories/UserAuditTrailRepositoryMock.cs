using PortoGO.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortoGO.DB.Domain;
using System.Linq.Expressions;

namespace PortoGO.DB.Tests.Mocks.Repositories
{
    public class UserAuditTrailRepositoryMock : IUserAuditTrailRepository
    {
        private IQueryable<UserAuditTrail> audit;

        public UserAuditTrailRepositoryMock()
        {
            Setup();
        }

        private void Setup()
        {
            var auditList = new List<UserAuditTrail>();

            auditList.Add(new UserAuditTrail
            {
                Id = 1,
                TimeStamp = DateTime.Now.AddHours(-4),
                Type = UserAuditTrailType.Add,
                UserName = "Batatinha"
            });

            auditList.Add(new UserAuditTrail
            {
                Id = 2,
                TimeStamp = DateTime.Now.AddHours(-3),
                Type = UserAuditTrailType.Add,
                UserName = "Companhia"
            });

            auditList.Add(new UserAuditTrail
            {
                Id = 3,
                TimeStamp = DateTime.Now.AddHours(-2),
                Type = UserAuditTrailType.Remove,
                UserName = "Batatinha"
            });

            auditList.Add(new UserAuditTrail
            {
                Id = 4,
                TimeStamp = DateTime.Now.AddHours(-1),
                Type = UserAuditTrailType.Remove,
                UserName = "Companhia"
            });

            this.audit = auditList.AsQueryable();
        }

        public int Count()
        {
            return this.audit.Count();
        }

        public int Count(Expression<Func<UserAuditTrail, bool>> predicate)
        {
            return this.audit.Count(predicate);
        }

        public void Delete(int id)
        {
            var item = this.Get(id);

            this.Delete(item);
        }

        public void Delete(UserAuditTrail entity)
        {
            var data = this.audit.ToList();

            data.Remove(entity);

            this.audit = data.AsQueryable();
        }

        public IEnumerable<UserAuditTrail> Find(Expression<Func<UserAuditTrail, bool>> predicate)
        {
            return this.audit.Where(predicate);
        }

        public IEnumerable<UserAuditTrail> Find(Expression<Func<UserAuditTrail, bool>> predicate, Func<IQueryable<UserAuditTrail>, IOrderedQueryable<UserAuditTrail>> orderBy = null, string includeProperties = "")
        {
            return this.audit.Where(predicate);
        }

        public UserAuditTrail FirstOrDefault(Expression<Func<UserAuditTrail, bool>> predicate)
        {
            return this.audit.Where(predicate).FirstOrDefault();
        }

        public UserAuditTrail Get(int id)
        {
            var item = this.audit.Where(x => x.Id == id).FirstOrDefault();

            return item;
        }

        public IEnumerable<UserAuditTrail> GetAll()
        {
            return this.audit;
        }

        public IEnumerable<UserAuditTrail> GetAll(string includeProperties = "")
        {
            return this.audit;
        }

        public UserAuditTrail Insert(UserAuditTrail entity)
        {
            var data = this.audit.ToList();

            entity.Id = data.Count + 1;

            data.Add(entity);

            this.audit = data.AsQueryable();

            return entity;
        }

        public UserAuditTrail InsertOrUpdate(UserAuditTrail entity)
        {
            throw new NotImplementedException();
        }

        public void LogAccountCreation(User user)
        {
            var log = new UserAuditTrail
            {
                TimeStamp = DateTime.Now,
                Type = UserAuditTrailType.Add,
                UserName = user.UserName
            };

            this.Insert(log);
        }

        public void LogAccountDeletion(User user)
        {
            var log = new UserAuditTrail
            {
                TimeStamp = DateTime.Now,
                Type = UserAuditTrailType.Remove,
                UserName = user.UserName
            };

            this.Insert(log);
        }

        public void Update(UserAuditTrail entity)
        {
            var oldItem = this.Get(entity.Id);

            var data = this.audit.ToList();

            data.Remove(oldItem);

            data.Add(entity);

            this.audit = data.AsQueryable();
        }
    }
}
