using School.Data.DataContexts;
using School.Domain.Contracts.Repositories;
using School.Domain.Models;
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

        public IEnumerable<Person> Get()
        {
            return _context.People.Where(p => p.PersonType == PersonType.Student);
        }

        public Person Get(int id)
        {
            return _context.People.Where(p => p.Id == id && p.PersonType == PersonType.Student).FirstOrDefault();
        }

        public void Create(Person entity)
        {
            _context.People.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Person entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Person entity)
        {
            //In this case the entity is just set as Inactive
            entity.Status = PersonStatus.Inactive;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
