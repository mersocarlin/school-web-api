using School.Data.DataContexts;
using School.Domain;
using School.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace School.Data.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private SchoolContext _context;

        public ProfessorRepository(SchoolContext context)
        {
            this._context = context;
        }

        public List<Professor> Get()
        {
            return _context.Professors.ToList();
        }

        public Professor Get(int id)
        {
            return _context.Professors.Where(p => p.Id == id).FirstOrDefault();
        }

        public void Create(Professor entity)
        {
            _context.Professors.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Professor entity)
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
