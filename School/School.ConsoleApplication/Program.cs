using Microsoft.Practices.Unity;
using School.Data.DataContexts;
using School.Data.Repositories;
using School.Domain;
using School.Domain.Contracts;
using School.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.ConsoleApplication
{
    class Program
    {

        static void Main(string[] args)
        {
            UnityContainer container = new UnityContainer();
            DependencyResolver.Resolve(container);

            IStudentRepository _repStudent = container.Resolve<IStudentRepository>();
            IProfessorRepository _repProfessor = container.Resolve<IProfessorRepository>();
            Random rand = new Random();

            try
            {
                /***
                 * Creating random Students
                 ***/ 
                for (int i = 0; i < 1000; i++)
                    _repStudent.Create(CreatRandomStudent(rand));

                /***
                 * Creating random Professors
                 ***/
                for (int i = 0; i < 30; i++)
                    _repProfessor.Create(new Professor
                    {
                        Person = CreateRandoPerson(rand, PersonType.Professor)
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static Student CreatRandomStudent(Random rand)
        {
            return new Student
            {
                Person = CreateRandoPerson(rand, PersonType.Student),
                Height = rand.Next(140, 181)
            };
        }

        public static Person CreateRandoPerson(Random rand, PersonType PersonType)
        {
            return new Person
            {
                Address = "Adress number " + rand.Next(0, 99999),
                DateOfBirth = new DateTime(rand.Next(1990, 2001), rand.Next(1, 13), rand.Next(1, 29)),
                FirstName = NameGenerator.FirsNames[rand.Next(0, NameGenerator.FirsNames.Length)],
                LastName = NameGenerator.LastNames[rand.Next(0, NameGenerator.LastNames.Length)],
                UserName = "user" + rand.Next(0, 99999),
                Password = "pass" + rand.Next(0, 99999),
                PersonType = PersonType
            };
        }
    }

    public static class DependencyResolver
    {
        public static void Resolve(UnityContainer container)
        {
            container.RegisterType<SchoolContext, SchoolContext>();
            container.RegisterType<IClassroomRepository, ClassroomRepository>();
            container.RegisterType<IProfessorRepository, ProfessorRepository>();
            container.RegisterType<IStudentRepository, StudentRepository>();
        }
    }
}
