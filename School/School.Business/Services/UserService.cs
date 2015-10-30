using School.Domain.Contracts.Repositories;
using School.Domain.Contracts.Services;
using School.Domain.Models;
using System;
using System.Linq;

namespace School.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            this._repository = repository;
        }

        public object GetUsers()
        {
            var users = this._repository.Get()
                .OrderBy(u => u.Username);

            return from user in users
                   select user.ToJson();
        }

        public User Authenticate(string username, string password)
        {
            return this._repository.Authenticate(username, password);
        }

        public User GetByRefreshTokenId(string refreshTokenId)
        {
            return this._repository.GetByRefreshTokenId(refreshTokenId);
        }

        public void UpdateLastLogin(int id)
        {
            var user = this._repository.GetById(id);

            if (user == null) return;

            user.LastLogin = DateTime.Now;

            this._repository.Update(user);
        }

        public void UpdateRefreshToken(int id, string refreshTokenId, string protectedTicket)
        {
            var user = this._repository.GetById(id);

            if (user == null) return;

            user.RefreshTokenId = refreshTokenId;
            user.ProtectedTicket = protectedTicket;

            this._repository.Update(user);
        }

        public void SaveUser(User user)
        {
            user.Validate();

            User newUser = user.Id == 0 ? new User() : this._repository.GetByIdWithProperties(user.Id);

            newUser.Profile = user.Profile;
            newUser.Username = user.Username;
            newUser.Password = User.EncryptPassword(user.Password);
            newUser.PersonId = user.PersonId;
            newUser.UpdatedAt = DateTime.Now;
            newUser.LastLogin = user.LastLogin;
            newUser.Status = user.Status;

            if (newUser.Id == 0)
                this._repository.Create(newUser);
            else
                this._repository.Update(newUser);
        }

        public void Dispose()
        {
            this._repository.Dispose();
        }
    }
}
