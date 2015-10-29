using School.Domain.Contracts.Repositories;
using School.Domain.Contracts.Services;
using School.Domain.Models;
using System;
using System.Linq;

namespace School.Business.Services
{
    public class CourseService : ICourseService
    {
        private ICourseRepository _repository;

        public CourseService(ICourseRepository repository)
        {
            this._repository = repository;
        }
        public object Get()
        {
            var collection = this._repository.Get()
                .Where(c => c.Status == EntityStatus.Active)
                .OrderBy(c => c.Name);

            if (collection == null) return null;

            return from course in collection
                   select course.ToJson();
        }

        public Course GetCourseById(int id)
        {
            return this._repository.GetById(id);
        }

        public void Save(int userId, Course course)
        {
            course.Validate();

            Course newCourse = course.Id == 0 ? new Course() : this._repository.GetById(course.Id);

            newCourse.UpdatedById = userId;
            newCourse.UpdatedAt = DateTime.Now;
            newCourse.Status = course.Status;
            newCourse.Name = course.Name;

            if (newCourse.Id == 0)
            {
                newCourse.CreatedById = userId;
                this._repository.Create(newCourse);
            }
            else
            {
                this._repository.Update(newCourse);
            }
        }

        public void Dispose()
        {
            this._repository.Dispose();
        }
    }
}
