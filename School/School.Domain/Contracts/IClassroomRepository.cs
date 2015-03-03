using System.Collections.Generic;

namespace School.Domain.Contracts
{
    public interface IClassroomRepository : IRepository<Classroom>
    {
        Classroom GetWithStudents(int id);
        List<Classroom> GetWithStudents();
        bool IsEmpty();
    }
}
