using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CitcWeb.Repository.Base
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(Expression<Func<TEntity,bool>> filter);
        TEntity GetById(params object[] objects);
        TEntity GetSingle(Expression<Func<TEntity, bool>> filter);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity form, TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(Expression<Func<TEntity, bool>> predicate);
    }
}