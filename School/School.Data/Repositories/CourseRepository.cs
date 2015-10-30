using School.Data.DataContexts;
using School.Domain.Contracts.Repositories;
using School.Domain.Models;

namespace School.Data.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(SchoolContext context)
            : base (context)
        {
        
        }
    }
}
