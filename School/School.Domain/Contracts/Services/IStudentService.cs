using School.Domain.Models;
using System;
using System.Collections.Generic;

namespace School.Domain.Contracts.Services
{
    public interface IStudentService : IDisposable
    {
        void Save(Person student);
        void Delete(int id);
        Person GetById(int id);
        IEnumerable<Person> Get();
    }
}
