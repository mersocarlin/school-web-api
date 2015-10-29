using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace School.Domain.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> GetIncluding(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetById(int id);
        TEntity GetFirstByIncluding(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Dispose();
    }
}
