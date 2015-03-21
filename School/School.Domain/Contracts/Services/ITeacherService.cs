using School.Domain.Models;
using System;
using System.Collections.Generic;

namespace School.Domain.Contracts.Services
{
    public interface ITeacherService : IDisposable
    {
        void Save(Person teacher);
        Person GetById(int id);
        IEnumerable<Person> Get();
    }
}
