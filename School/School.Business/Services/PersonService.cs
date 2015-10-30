using School.Domain.Contracts.Repositories;
using School.Domain.Contracts.Services;
using School.Domain.Models;
using System;
using System.Linq;

namespace School.Business.Services
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            this._repository = repository;
        }

        private bool PersonTypePredicate(Person person, int personType)
        {
            switch (personType)
            {
                default:
                    return true;
                case (int)PersonType.Student:
                    return person.PersonType == PersonType.Student || person.PersonType == PersonType.StudentAndTeacher;
                case (int)PersonType.Teacher:
                    return person.PersonType == PersonType.Teacher || person.PersonType == PersonType.StudentAndTeacher;
                case (int)PersonType.StudentAndTeacher:
                    return person.PersonType == PersonType.StudentAndTeacher;
            }
        }

        private bool PersonStatusPredicate(Person person, int personStatus)
        {
            return personStatus == -1 || (int)person.Status == personStatus;
        }

        private bool PersonQuerySearch(Person person, string query)
        {
            return person.Name.ToUpper().Contains(query) || (!string.IsNullOrEmpty(person.Email) && person.Email.ToUpper().Contains(query));
        }

        public object Get(string query, int personType, int personStatus, int page, int pageSize = 50)
        {
            if (string.IsNullOrEmpty(query))
            {
                query = "";
            }

            query = query.ToUpper();

            var collection = this._repository
                .Get()
                .Where(p => PersonTypePredicate(p, personType) && PersonStatusPredicate(p, personStatus) && PersonQuerySearch(p, query));

            int total = collection.Count();

            collection = collection
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return new
            {
                page = page,
                total = total,
                data = from p in collection
                       select p.ToJson(),
            };
        }

        public Person GetPersonById(int id)
        {
            return this._repository.GetById(id);
        }

        public void SavePerson(int userId, Person person)
        {
            person.Validate();

            Person newPerson = person.Id == 0 ? new Person() : this._repository.GetByIdWithProperties(person.Id);

            newPerson.UpdatedById = userId;
            newPerson.UpdatedAt = DateTime.Now;
            newPerson.PersonType = person.PersonType;

            if (newPerson.IsTeacher())
            {
                newPerson.Status = person.Status;
            }

            newPerson.Name = person.Name;
            newPerson.Gender = person.Gender;
            newPerson.DateOfBirth = person.DateOfBirth;
            newPerson.Email = person.Email;
            newPerson.HomePhone = person.HomePhone;
            newPerson.MobilePhone = person.MobilePhone;
            newPerson.WorkPhone = person.WorkPhone;
            newPerson.Address = person.Address;

            if (newPerson.Id == 0)
            {
                newPerson.CreatedById = userId;
                this._repository.Create(newPerson);
            }
            else
            {
                this._repository.Update(newPerson);
            }
        }

        public void Dispose()
        {
            this._repository.Dispose();
        }
    }
}
