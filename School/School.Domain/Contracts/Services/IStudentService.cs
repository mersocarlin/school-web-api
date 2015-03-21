using School.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Contracts.Services
{
    public interface IStudentService : IDisposable
    {
        void Save(Person student);
        Person GetById(int id);
        IEnumerable<Person> Get();
    }
}
