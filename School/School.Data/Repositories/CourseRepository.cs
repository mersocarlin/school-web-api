using School.Data.DataContexts;
using School.Domain.Contracts;
using School.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace School.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private SchoolContext _context;

        public CourseRepository(SchoolContext context)
        {
            this._context = context;
        }

        public IEnumerable<Course> Get()
        {
            return _context.Courses;
        }

        public Course Get(int id)
        {
            return _context.Courses.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Create(Course entity)
        {
            _context.Courses.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Course entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
