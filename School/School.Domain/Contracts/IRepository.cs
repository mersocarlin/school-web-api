using System;
using System.Collections.Generic;

namespace School.Domain.Contracts
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> Get();
        T Get(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
