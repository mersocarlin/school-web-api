using School.Data.DataContexts;
using School.Domain.Contracts.Repositories;
using School.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace School.Data.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(SchoolContext context)
            : base (context)
        {
        
        }

        public void Delete(Course entity)
        {
            //In this case the entity is just set as Inactive
            entity.Status = EntityStatus.Inactive;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
