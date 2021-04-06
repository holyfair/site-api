using Repository.DatabaseModels;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);

        Task<User> GetUserAsync(string email);
    }
}
