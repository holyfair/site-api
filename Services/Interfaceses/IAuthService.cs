using Models.Auth;
using System.Threading.Tasks;

namespace Services.Interfaceses
{
    public interface IAuthService
    {
        Task<string> BuildTokenAsync(LoginModel loginModel);
    }
}
