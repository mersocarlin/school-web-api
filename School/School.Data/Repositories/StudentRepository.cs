using School.Data.DataContexts;
using School.Domain.Contracts.Repositories;
using School.Domain.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace School.Data.Repositories
{
    public class StudentRepository : Repository<Person>, IStudentRepository
    {
        public StudentRepository(SchoolContext context)
            : base(context)
        {

        }

        public IEnumerable<Person> GetStudents()
        {
            return _context.People.Where(p => p.PersonType == PersonType.Student || p.PersonType == PersonType.StudentAndTeacher);
        }

        public override void Delete(Person entity)
        {
            //In this case the entity is just set as Inactive
            entity.Status = EntityStatus.Inactive;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
