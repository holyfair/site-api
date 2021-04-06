using System.Threading.Tasks;
using Models;
using Models.Exceptions;
using Repository.DatabaseModels;
using Repository.UnitOfWork;
using Services.Helpers;
using Services.Interfaceses;

namespace Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUserAsync(UserDTO model)
        {
            var userRecord = await _unitOfWork.Users.GetUserAsync(model.Email);
            if(userRecord != null)
            {
                throw new BadRequestException("User with this email alredy exists");
            }
            var encodedPassword = PasswordHasher.EncodePasswordToBase64(model.Password);
            await _unitOfWork.Users.CreateUserAsync(new User { Email = model.Email, Password = encodedPassword });
            await _unitOfWork.CommitAsync();
        }
    }
}
