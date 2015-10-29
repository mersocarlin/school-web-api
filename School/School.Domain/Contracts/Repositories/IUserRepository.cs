using School.Domain.Models;

namespace School.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByIdWithProperties(int id);
        User Authenticate(string username, string password);
        User GetByRefreshTokenId(string refreshTokenId);
    }
}
