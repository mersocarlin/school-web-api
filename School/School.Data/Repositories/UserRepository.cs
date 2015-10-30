using School.Data.DataContexts;
using School.Domain.Contracts.Repositories;
using School.Domain.Models;
using System.Linq;

namespace School.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SchoolContext context)
            : base(context)
        {

        }

        public User GetByIdWithProperties(int id)
        {
            return this.GetFirstByIncluding(
                u => u.Id == id,
                u => u.Person);
        }

        public User Authenticate(string username, string password)
        {
            password = User.EncryptPassword(password);

            return this.GetFirstByIncluding(
               u => u.Username.Equals(username) &&
                    u.Password.Equals(password),
               u => u.Person);
        }

        public User GetByRefreshTokenId(string refreshTokenId)
        {
            return this._context
                .Users
                .Where(u => u.RefreshTokenId.Equals(refreshTokenId)).FirstOrDefault();
        }
    }
}
