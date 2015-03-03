using School.Data.DataContexts;
using School.Domain;
using School.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace School.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            this._context = context;
        }

        public List<Student> Get()
        {
            return _context.Students.ToList();
        }

        public Student Get(int id)
        {
            return _context.Students.Where(s => s.Id == id).FirstOrDefault();
        }

        public void Create(Student entity)
        {
            _context.Students.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Student entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetWithClassroom()
        {
            return _context.Students.Include(s => s.Classroom).ToList();
        }

        public Student GetWithClassroom(int id)
        {
            return _context.Students.Where(s => s.Id == id).Include(s => s.Classroom).FirstOrDefault();
        }

        public List<Student> GetWithPerson()
        {
            return _context.Students.Include(s => s.Person).ToList();
        }

        public Student GetWithPerson(int id)
        {
            return _context.Students.Where(s => s.Id == id).Include(s => s.Person).FirstOrDefault();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
