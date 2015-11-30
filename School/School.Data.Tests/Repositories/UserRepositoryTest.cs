using Moq;
using NUnit.Framework;
using School.Domain.Contracts.Repositories;
using School.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace School.Data.Tests.Repositories
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private List<User> userDatabase;

        private Mock<IUserRepository> mockUserRepository;

        private IUserRepository userRepository;

        [SetUp]
        public void Setup()
        {
            this.SetupDatabase();
            this.SetupMock();
        }

        private void SetupDatabase()
        {
            this.userDatabase = new List<User>();
            for (int i = 1; i < 11; i++)
            {
                var User = new User
                {
                    Id = i,
                    Password = "password" + i,
                    Username = "user" + i
                };

                this.userDatabase.Add(User);
            }
        }

        public void SetupMock()
        {
            this.mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);

            this.mockUserRepository
                .Setup(repo => repo.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string username, string password) =>
                {
                    return this.userDatabase
                        .Where(u => u.Username.Equals(username) && u.Password.Equals(password))
                        .FirstOrDefault();
                });

            this.mockUserRepository
                .Setup(repo => repo.GetByIdWithProperties(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return this.userDatabase.Where(p => p.Id == id).FirstOrDefault();
                });

            this.userRepository = this.mockUserRepository.Object;
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        public void GetUserById_OnValidId_ReturnsValidUser(int id)
        {
            var user = this.userRepository.GetByIdWithProperties(id);

            Assert.NotNull(user);
            Assert.IsInstanceOf<User>(user);
            Assert.AreEqual(id, user.Id);
        }

        [Test]
        [TestCase(50)]
        [TestCase(51)]
        public void GetUserById_OnInvalidId_ReturnsNull(int id)
        {
            var user = this.userRepository.GetByIdWithProperties(id);

            Assert.IsNull(user);
        }

        [Test]
        [TestCase("user2", "password2")]
        [TestCase("user3", "password3")]
        public void Authenticate_OnValidUsernameAndPassword_MustAuthenticate(string username, string password)
        {
            var user = this.userRepository.Authenticate(username, password);

            this.mockUserRepository.Verify(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.NotNull(user);
            Assert.IsInstanceOf<User>(user);
        }

        [Test]
        [TestCase("abc", "abc")]
        [TestCase("def", "def")]
        public void Authenticate_OnInValidUsernameAndPassword_ReturnsNull(string username, string password)
        {
            var user = this.userRepository.Authenticate(username, password);

            this.mockUserRepository.Verify(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsNull(user);
        }

        [TearDown]
        public void TearDown()
        {
            this.userDatabase.Clear();
            this.userDatabase = null;

            this.mockUserRepository = null;

            this.userRepository = null;
        }
    }
}
