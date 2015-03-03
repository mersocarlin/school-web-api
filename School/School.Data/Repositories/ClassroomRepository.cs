using School.Data.DataContexts;
using School.Domain;
using School.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace School.Data.Repositories
{
    public class ClassroomRepository : IClassroomRepository
    {
        SchoolContext _db;

        public ClassroomRepository(SchoolContext context)
        {
            this._db = context;
        }

        public List<Classroom> Get()
        {
            return _db.Classrooms.ToList();
        }

        public Classroom Get(int id)
        {
            return _db.Classrooms.Where(cr => cr.Id == id).FirstOrDefault();
        }

        public void Create(Classroom entity)
        {
            _db.Classrooms.Add(entity);
            _db.SaveChanges();
        }

        public void Update(Classroom entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Classroom GetWithStudents(int id)
        {
            return _db.Classrooms.Where(cr => cr.Id == id).Include(cr => cr.Students.Select(s => s.Person)).FirstOrDefault();
        }

        public List<Classroom> GetWithStudents()
        {
            return _db.Classrooms.Include(cr => cr.Students.Select(s => s.Person)).ToList();
        }

        public bool IsEmpty()
        {
            return _db.Classrooms.Count() == 0;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
