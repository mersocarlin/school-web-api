using School.Domain.Models;
using System.Collections.Generic;

namespace School.Domain.Contracts.Repositories
{
    public interface ITeacherRepository : IRepository<Person>
    {
        IEnumerable<Person> GetTeachers();
    }
}
