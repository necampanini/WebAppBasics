using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Repositories;
using Models.Entities;

namespace Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return null;

            return await _appDbContext.Users
                .Where(x => x.Email == email)
                .SingleOrDefaultAsync();
        }

        public async Task<int> InsertUser(User user)
        {
            //todo: make sure 'user' not null;
            _appDbContext.Users.Add(user);

            return await _appDbContext.SaveChangesAsync();
        }
    }
}