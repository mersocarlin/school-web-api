using School.Domain.Contracts.Repositories;
using School.Domain.Contracts.Services;
using School.Domain.Models;
using System.Collections.Generic;

namespace School.Business.Services
{
    public class TeacherService : ITeacherService
    {
        private ITeacherRepository _repository;

        public TeacherService(ITeacherRepository repository)
        {
            this._repository = repository;
        }

        public void Save(Person teacher)
        {
            teacher.Validate();
            if (teacher.Id == 0)
                _repository.Create(teacher);
            else
                _repository.Update(teacher);
        }

        public Person GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Person> Get()
        {
            return _repository.GetTeachers();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
