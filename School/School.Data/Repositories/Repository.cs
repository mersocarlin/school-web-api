using School.Data.DataContexts;
using School.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace School.Data.Repositories
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        protected SchoolContext _context;

        public Repository(SchoolContext context)
        {
            this._context = context;
        }

        public IEnumerable<TEntity> Get()
        {
            return _context.Set<TEntity>().AsEnumerable();
        }

        public IEnumerable<TEntity> GetIncluding(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                    (_context.Set<TEntity>(), (current, expression) => current.Include(expression)).Where(predicate).AsEnumerable<TEntity>();
        }

        public virtual TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public TEntity GetFirstByIncluding(Func<TEntity, bool> predicate, params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                (_context.Set<TEntity>(), (current, expression) => current.Include(expression)).Where(predicate).FirstOrDefault();
        }

        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            //_context.Set<TEntity>().Remove(entity);
            //_context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
