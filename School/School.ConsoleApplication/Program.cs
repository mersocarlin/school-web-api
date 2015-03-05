using Microsoft.Practices.Unity;
using School.Data.DataContexts;
using School.Data.Repositories;
using School.Domain.Contracts;
using School.Domain.Models;
using School.Utils;
using System;

namespace School.ConsoleApplication
{
    class Program
    {
        const int MAX_RAND_VALUE = 99999;

        static void Main(string[] args)
        {
            UnityContainer container = new UnityContainer();
            DependencyResolver.Resolve(container);

            IStudentRepository _repStudent = container.Resolve<IStudentRepository>();
            ITeacherRepository _repTeacher = container.Resolve<ITeacherRepository>();
            Random rand = new Random();

            try
            {
                /***
                 * Creating random Students
                 ***/ 
                for (int i = 0; i < 500; i++)
                    _repStudent.Create(CreateRandoPerson(rand, PersonType.Student));

                /***
                 * Creating random Professors
                 ***/
                for (int i = 0; i < 30; i++)
                    _repTeacher.Create(CreateRandoPerson(rand, PersonType.Teacher));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static Person CreateRandoPerson(Random rand, PersonType PersonType)
        {
            var person = new Person
            {
                Status = rand.Next(0, MAX_RAND_VALUE) % 2 == 0 ? PersonStatus.Active : PersonStatus.Inactive,
                FirstName = NameGenerator.FirsNames[rand.Next(0, NameGenerator.FirsNames.Length)],
                LastName = NameGenerator.LastNames[rand.Next(0, NameGenerator.LastNames.Length)],
                Gender = rand.Next(0, MAX_RAND_VALUE) % 2 == 0 ? GenderType.Male : GenderType.Female,
                DateOfBirth = new DateTime(rand.Next(1990, 2001), rand.Next(1, 13), rand.Next(1, 29)),
                Password = "pass" + rand.Next(0, MAX_RAND_VALUE),
                Address = "Adress number " + rand.Next(0, MAX_RAND_VALUE),
                PersonType = PersonType
            };

            person.Email = person.FirstName.ToLower() + "@mail.com";
            return person;
        }
    }

    public static class DependencyResolver
    {
        public static void Resolve(UnityContainer container)
        {
            container.RegisterType<SchoolContext, SchoolContext>();
            container.RegisterType<ICourseRepository, CourseRepository>();
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType<ITeacherRepository, TeacherRepository>();
        }
    }
}
