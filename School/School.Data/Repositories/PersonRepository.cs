using School.Data.DataContexts;
using School.Domain.Contracts.Repositories;
using School.Domain.Models;

namespace School.Data.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(SchoolContext context)
            : base(context)
        {

        }

        public Person GetByIdWithProperties(int id)
        {
            return this.GetFirstByIncluding(
                p => p.Id == id);
        }
    }
}
