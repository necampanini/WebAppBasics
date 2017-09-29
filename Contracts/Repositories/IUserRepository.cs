using System.Threading.Tasks;
using Models.Entities;

namespace Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<int> InsertUser(User user);
    }
}