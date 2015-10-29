using School.Domain.Models;

namespace School.Domain.Contracts.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person GetByIdWithProperties(int id);
    }
}
