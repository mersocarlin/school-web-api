using School.Domain.Models;

namespace School.Domain.Contracts
{
    public interface ICourseRepository : IRepository<Course>
    {
        //Course GetWithStudents(int id);
        //List<Course> GetWithStudents();
        //bool IsEmpty();
    }
}
