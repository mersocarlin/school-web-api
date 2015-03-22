using School.Domain.Models;
using System.Collections.Generic;

namespace School.Domain.Contracts.Repositories
{
    public interface IStudentRepository : IRepository<Person>
    {
        IEnumerable<Person> GetStudents();
    }
}
