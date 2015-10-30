using School.Domain.Models;
using System;

namespace School.Domain.Contracts.Services
{
    public interface IPersonService : IDisposable
    {
        object Get(string query, int personType, int personStatus, int page, int pageSize = 50);
        Person GetPersonById(int id);
        void SavePerson(int userId, Person person);
    }
}
