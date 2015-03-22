using School.Data.DataContexts;
using School.Domain.Contracts.Repositories;
using School.Domain.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace School.Data.Repositories
{
    public class TeacherRepository : Repository<Person>, ITeacherRepository
    {
        public TeacherRepository(SchoolContext context)
            : base(context)
        {

        }

        public override void Delete(Person entity)
        {
            entity.Status = EntityStatus.Inactive;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<Person> GetTeachers()
        {
            return _context.People.Where(p => p.PersonType == PersonType.Teacher || p.PersonType == PersonType.StudentAndTeacher);
        }
    }
}
