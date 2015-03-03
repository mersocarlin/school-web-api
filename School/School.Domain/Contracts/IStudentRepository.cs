using System.Collections.Generic;

namespace School.Domain.Contracts
{
    public interface IStudentRepository : IRepository<Student>
    {
        List<Student> GetWithClassroom();
        Student GetWithClassroom(int id);
        List<Student> GetWithPerson();
        Student GetWithPerson(int id);
    }
}
