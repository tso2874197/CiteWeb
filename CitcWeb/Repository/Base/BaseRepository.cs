using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CitcWeb.Repository.Base
{
    public abstract class BaseRepository<T>:IRepository<T> where T:class
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<T> BaseDbSet;

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            DbContext = unitOfWork.Context;
            BaseDbSet = DbContext.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return BaseDbSet.AsQueryable();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter)
        {
            return BaseDbSet.Where(filter);
        }

        public T GetById(params object[] objects)
        {
            return BaseDbSet.Find(objects);
        }

        public T GetSingle(Expression<Func<T, bool>> filter)
        {
            return BaseDbSet.SingleOrDefault(filter);
        }

        public void Add(T entity)
        {
            BaseDbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            BaseDbSet.AddRange(entities);
        }

        public void Update(T form, T entity)
        {
            DbContext.Entry(entity).CurrentValues.SetValues(form);
        }

        public void Update(T entity)
        {
            BaseDbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            BaseDbSet.Remove(entity);
        }

        public void Remove(Expression<Func<T, bool>> predicate)
        {
            var query = DbContext.Set<T>().Where(predicate);
            BaseDbSet.RemoveRange(query);
        }
    }
}