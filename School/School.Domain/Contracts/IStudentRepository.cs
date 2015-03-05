using School.Domain.Models;

namespace School.Domain.Contracts
{
    public interface IStudentRepository : IRepository<Person>
    {
        //List<Person> GetWithClassroom();
        //Person GetWithClassroom(int id);
    }
}
