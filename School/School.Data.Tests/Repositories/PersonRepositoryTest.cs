using Moq;
using NUnit.Framework;
using School.Domain.Contracts.Repositories;
using School.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace School.Data.Tests.Repositories
{
    [TestFixture]
    public class PersonRepositoryTest
    {
        private List<Person> personDatabse;

        private Mock<IPersonRepository> mockPersonRepository;

        private IPersonRepository personRepository;

        [SetUp]
        public void Setup()
        {
            this.SetupDatabase();
            this.SetupMock();
        }

        private void SetupDatabase()
        {
            this.personDatabse = new List<Person>();
            for (int i = 1; i < 11; i++)
            {
                var person = new Person
                {
                    Id = i,
                    Name = "Person Name " + i
                };

                this.personDatabse.Add(person);
            }
        }

        public void SetupMock()
        {
            this.mockPersonRepository = new Mock<IPersonRepository>(MockBehavior.Strict);

            this.mockPersonRepository
                .Setup(repo => repo.Create(It.IsAny<Person>()))
                .Callback((Person person) =>
                {
                    this.personDatabse.Add(person);
                });

            this.mockPersonRepository
                .Setup(repo => repo.Get())
                .Returns(() =>
                {
                    return this.personDatabse;
                });

            this.mockPersonRepository
                .Setup(repo => repo.GetByIdWithProperties(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return this.personDatabse.Where(p => p.Id == id).FirstOrDefault();
                });

            this.mockPersonRepository
                .Setup(repo => repo.Update(It.IsAny<Person>()))
                .Callback((Person person) =>
                {
                    int personIndex = this.personDatabse
                        .FindIndex(p => p.Id == person.Id);

                    this.personDatabse[personIndex] = person;
                });

            this.personRepository = this.mockPersonRepository.Object;
        }

        [Test]
        public void Create_OnValidPerson_InsertsInDatabase()
        {
            Person person = new Person();

            int initialPersonCount = this.personDatabse.Count;

            this.personRepository.Create(person);

            int expectedPersonCount = initialPersonCount + 1;

            this.mockPersonRepository.Verify(m => m.Create(It.IsAny<Person>()), Times.Once());
            Assert.AreEqual(expectedPersonCount, this.personDatabse.Count);
        }

        [Test]
        public void Get_OnCalled_ReturnsAllRecordsInDatabase()
        {
            IEnumerable<Person> personList = this.personRepository.Get();

            Assert.IsNotEmpty(personList);
            Assert.AreEqual(personList.Count(), personDatabse.Count);
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        public void GetPersonById_OnValidId_ReturnsValidPerson(int id)
        {
            var person = this.personRepository.GetByIdWithProperties(id);

            Assert.NotNull(person);
            Assert.IsInstanceOf<Person>(person);
            Assert.AreEqual(id, person.Id);
        }

        [Test]
        [TestCase(50)]
        [TestCase(51)]
        public void GetPersonById_OnInvalidId_ReturnsNull(int id)
        {
            var person = this.personRepository.GetByIdWithProperties(id);

            Assert.IsNull(person);
        }

        [Test]
        [TestCase(6)]
        [TestCase(8)]
        public void Update_OnValidPerson_UpdatesIt(int id)
        {
            var person = this.personRepository.GetByIdWithProperties(id);

            person.Name = String.Format("Person Id = {0} Name changed!", id);

            this.personRepository.Update(person);

            var expectedPerson = this.personRepository.GetByIdWithProperties(id);

            this.mockPersonRepository.Verify(m => m.Update(It.IsAny<Person>()), Times.Once());
            Assert.NotNull(expectedPerson);
            Assert.AreEqual(expectedPerson.Name, person.Name);
        }

        [TearDown]
        public void TearDown()
        {
            this.personDatabse.Clear();
            this.personDatabse = null;

            this.mockPersonRepository = null;

            this.personRepository = null;
        }
    }
}
