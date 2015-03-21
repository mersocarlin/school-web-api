using School.Domain.Contracts.Repositories;
using School.Domain.Contracts.Services;
using School.Domain.Models;
using System;
using System.Collections.Generic;

namespace School.Business.Services
{
    public class StudentService : IStudentService
    {
        private IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            this._repository = repository;
        }

        public void Save(Person student)
        {
            student.Validate();
            if (student.Id == 0)
                _repository.Create(student);
            else
                _repository.Update(student);
        }

        public void Delete(int id)
        {
            var student = GetById(id);
            if(student == null)
                throw new Exception("Student does not exist in database");
            _repository.Delete(student);
        }

        public Person GetById(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Person> Get()
        {
            return _repository.Get();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
