using School.Domain.Models;
using System;

namespace School.Domain.Contracts.Services
{
    public interface IUserService : IDisposable
    {
        object GetUsers();
        User Authenticate(string username, string password);
        User GetByRefreshTokenId(string refreshTokenId);
        void UpdateLastLogin(int id);
        void UpdateRefreshToken(int id, string refreshTokenId, string protectedTicket);
        void SaveUser(User user);
    }
}
