using School.Data.DataContexts;
using School.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
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
