using School.Domain.Models;
using System;

namespace School.Domain.Contracts.Services
{
    public interface ICourseService : IDisposable
    {
        object Get();
        Course GetCourseById(int id);
        void Save(Course course);
    }
}
