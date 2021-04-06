using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.DatabaseModels;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private DbSet<User> _dbSet;

        public UserRepository(AppDbContext context)
        {
            _dbSet = context.Users;
        }

        public async Task CreateUserAsync(User user)
        {
            await _dbSet.AddAsync(user);
        }

        public async Task<User> GetUserAsync(string email)
        {
            var result = await _dbSet.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            return result;
        }
    }
}