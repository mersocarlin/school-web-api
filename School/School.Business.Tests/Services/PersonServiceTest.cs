using Moq;
using NUnit.Framework;
using School.Business.Models;
using School.Business.Services;
using School.Domain.Contracts.Repositories;
using School.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace School.Business.Tests.Services
{
    [TestFixture]
    public class PersonServiceTest
    {
        private const int PAGE = 1;
        private const int PAGE_SIZE = 50;

        private List<Person> personDatabase;
        private Mock<IPersonRepository> mockPersonRepository;

        private PersonService personService;

        [SetUp]
        public void Setup()
        {
            this.SetupDatabase();
            this.SetupMock();

            this.personService = new PersonService(this.mockPersonRepository.Object);
        }

        private void SetupDatabase()
        {
            this.personDatabase = new List<Person>()
            {
                new Person { Id = 1, Name = "Jamie Roberson", PersonType = PersonType.Student, Status = EntityStatus.Active },
                new Person { Id = 2, Name = "Marian	Hamilton", PersonType = PersonType.Student, Status = EntityStatus.Active },
                new Person { Id = 3, Name = "Lorenzo Dawson", PersonType = PersonType.Student, Status = EntityStatus.Active },
                new Person { Id = 4, Name = "Daisy Norris", PersonType = PersonType.Student, Status = EntityStatus.Inactive },
                new Person { Id = 5, Name = "Tommy Torres", PersonType = PersonType.Student, Status = EntityStatus.Inactive },

                new Person { Id = 6, Name = "Paul Frazier", PersonType = PersonType.Teacher, Status = EntityStatus.Active },
                new Person { Id = 7, Name = "Orlando Powers", PersonType = PersonType.Teacher, Status = EntityStatus.Active },
                new Person { Id = 8, Name = "Evelyn	Joseph", PersonType = PersonType.Teacher, Status = EntityStatus.Active },
                new Person { Id = 9, Name = "Roland	Mckinney", PersonType = PersonType.Teacher, Status = EntityStatus.Inactive },
                new Person { Id = 10, Name = "Eula Neal", PersonType = PersonType.Teacher, Status = EntityStatus.Inactive },
                new Person { Id = 11, Name = "Amber Turner", PersonType = PersonType.Teacher, Status = EntityStatus.Inactive },

                new Person { Id = 12, Name = "Jack Hamilton", PersonType = PersonType.StudentAndTeacher, Status = EntityStatus.Active },
                new Person { Id = 13, Name = "Paul Buchanan", PersonType = PersonType.StudentAndTeacher, Status = EntityStatus.Active },
                new Person { Id = 14, Name = "Colin	Gibson", PersonType = PersonType.StudentAndTeacher, Status = EntityStatus.Inactive }
            };
        }

        private void SetupMock()
        {
            this.mockPersonRepository = new Mock<IPersonRepository>(MockBehavior.Strict);

            this.mockPersonRepository
                .Setup(repo => repo.Get())
                .Returns(() => this.personDatabase);

            this.mockPersonRepository
                .Setup(repo => repo.GetByIdWithProperties(It.IsAny<int>()))
                .Returns((int id) => this.personDatabase.Where(p => p.Id == id).FirstOrDefault());

            this.mockPersonRepository
                .Setup(repo => repo.Create(It.IsAny<Person>()))
                .Callback(() => { });

            this.mockPersonRepository
                .Setup(repo => repo.Update(It.IsAny<Person>()))
                .Callback(() => { });
        }

        [Test]
        public void Get_OnEmptyQueryAndActive_ReturnsAllActivePersons()
        {
            object personPaginator = this.personService.Get("", -1, (int)EntityStatus.Active, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsNotEmpty(pp.Data);
            Assert.AreEqual(8, pp.Total);
        }

        [Test]
        public void Get_OnEmptyQueryInactive_ReturnsAllInactivePersons()
        {
            object personPaginator = this.personService.Get("", -1, (int)EntityStatus.Inactive, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsNotEmpty(pp.Data);
            Assert.AreEqual(6, pp.Total);
        }

        [Test]
        public void Get_OnEmptyQueryAndStudentAndActive_ReturnsOnlyActiveStudents()
        {
            object personPaginator = this.personService.Get("", (int)PersonType.Student, (int)EntityStatus.Active, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsNotEmpty(pp.Data);
            Assert.AreEqual(5, pp.Total);
        }

        [Test]
        public void Get_OnEmptyQueryAndStudentAndInactive_ReturnsOnlyInactiveStudents()
        {
            object personPaginator = this.personService.Get("", (int)PersonType.Student, (int)EntityStatus.Inactive, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsNotEmpty(pp.Data);
            Assert.AreEqual(3, pp.Total);
        }

        [Test]
        public void Get_OnEmptyQueryAndTeacherAndActive_ReturnsOnlyActiveTeachers()
        {
            object personPaginator = this.personService.Get("", (int)PersonType.Teacher, (int)EntityStatus.Active, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsNotEmpty(pp.Data);
            Assert.AreEqual(5, pp.Total);
        }

        [Test]
        public void Get_OnEmptyQueryAndTeacherAndInactive_ReturnsOnlyInactiveTeachers()
        {
            object personPaginator = this.personService.Get("", (int)PersonType.Teacher, (int)EntityStatus.Inactive, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsNotEmpty(pp.Data);
            Assert.AreEqual(4, pp.Total);
        }

        [Test]
        public void Get_OnEmptyQueryAndStudentTeacherAndActive_ReturnsOnlyActiveStudentTeachers()
        {
            object personPaginator = this.personService.Get("", (int)PersonType.StudentAndTeacher, (int)EntityStatus.Active, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsNotEmpty(pp.Data);
            Assert.AreEqual(2, pp.Total);
        }

        [Test]
        public void Get_OnEmptyQueryAndStudentTeacherAndInactive_ReturnsOnlyInactiveStudentTeachers()
        {
            object personPaginator = this.personService.Get("", (int)PersonType.StudentAndTeacher, (int)EntityStatus.Inactive, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsNotEmpty(pp.Data);
            Assert.AreEqual(1, pp.Total);
        }

        /*
         * filled | both    | active
         * filled | both    | inactive
         * dummy  | both    | whatever
         */

        [Test]
        public void Get_OnHamiltonAndActive_ReturnsAllActivePersonsMathingHamilton()
        {
            object personPaginator = this.personService.Get("Hamilton", -1, (int)EntityStatus.Active, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsNotEmpty(pp.Data);
            Assert.AreEqual(2, pp.Total);
        }

        [Test]
        public void Get_OnHamiltonAndInactive_ReturnsEmptyPaginator()
        {
            object personPaginator = this.personService.Get("Hamilton", -1, (int)EntityStatus.Inactive, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsEmpty(pp.Data);
            Assert.AreEqual(0, pp.Total);
        }

        [Test]
        public void Get_OnSpiderManAndActive_ReturnsEmptyPaginator()
        {
            object personPaginator = this.personService.Get("SpiderMan", -1, (int)EntityStatus.Active, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsEmpty(pp.Data);
            Assert.AreEqual(0, pp.Total);
        }

        [Test]
        public void Get_OnJackAndStudentAndActive_ReturnsAllActiveStudentsMathingJack()
        {
            object personPaginator = this.personService.Get("Jack", (int)PersonType.Student, (int)EntityStatus.Active, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsNotEmpty(pp.Data);
            Assert.AreEqual(1, pp.Total);
        }

        [Test]
        public void Get_OnJackAndStudentAndInactive_ReturnsEmptyPaginator()
        {
            object personPaginator = this.personService.Get("Jack", (int)PersonType.Student, (int)EntityStatus.Inactive, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsEmpty(pp.Data);
            Assert.AreEqual(0, pp.Total);
        }

        [Test]
        public void Get_OnSpiderManAndStudentAndActive_ReturnsEmptyPaginator()
        {
            object personPaginator = this.personService.Get("SpiderMan", (int)PersonType.Student, (int)EntityStatus.Active, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsEmpty(pp.Data);
            Assert.AreEqual(0, pp.Total);
        }

        [Test]
        public void Get_OnPaulAndTeacherAndActive_ReturnsAllActiveTeachersMathingPaul()
        {
            object personPaginator = this.personService.Get("Paul", (int)PersonType.Teacher, (int)EntityStatus.Active, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsNotEmpty(pp.Data);
            Assert.AreEqual(2, pp.Total);
        }

        [Test]
        public void Get_OnPaulAndTeacherAndInactive_ReturnsEmptyPaginator()
        {
            object personPaginator = this.personService.Get("Jack", (int)PersonType.Teacher, (int)EntityStatus.Inactive, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsEmpty(pp.Data);
            Assert.AreEqual(0, pp.Total);
        }

        [Test]
        public void Get_OnSpiderManAndTeacherAndActive_ReturnsEmptyPaginator()
        {
            object personPaginator = this.personService.Get("SpiderMan", (int)PersonType.Teacher, (int)EntityStatus.Active, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsEmpty(pp.Data);
            Assert.AreEqual(0, pp.Total);
        }

        [Test]
        public void Get_OnBuchananAndStudentTeacherAndActive_ReturnsAllActiveTeachersMathingBuchanan()
        {
            object personPaginator = this.personService.Get("Buchanan", (int)PersonType.StudentAndTeacher, (int)EntityStatus.Active, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsNotEmpty(pp.Data);
            Assert.AreEqual(1, pp.Total);
        }

        [Test]
        public void Get_OnBuchananAndStudentTeacherAndInactive_ReturnsEmptyPaginator()
        {
            object personPaginator = this.personService.Get("Buchanan", (int)PersonType.StudentAndTeacher, (int)EntityStatus.Inactive, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsEmpty(pp.Data);
            Assert.AreEqual(0, pp.Total);
        }

        [Test]
        public void Get_OnSpiderManAndStudentTeacherAndActive_ReturnsEmptyPaginator()
        {
            object personPaginator = this.personService.Get("SpiderMan", (int)PersonType.StudentAndTeacher, (int)EntityStatus.Active, PAGE, PAGE_SIZE);

            Assert.IsNotNull(personPaginator);
            Assert.IsInstanceOf<PersonPaginator>(personPaginator);

            PersonPaginator pp = personPaginator as PersonPaginator;

            Assert.IsEmpty(pp.Data);
            Assert.AreEqual(0, pp.Total);
        }

        [TearDown]
        public void TearDown()
        {
            this.personDatabase.Clear();
            this.mockPersonRepository = null;
            this.personService = null;
        }
    }
}
