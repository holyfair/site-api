using Repository.Interfaces;
using Repository.Repositories;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; set; }
        SourceRepository Sources { get; set; }
        Task CommitAsync();
    }
}