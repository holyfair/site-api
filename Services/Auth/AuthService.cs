using Models.Auth;
using Models.Exceptions;
using Repository.DatabaseModels;
using Repository.UnitOfWork;
using Services.Helpers;
using Services.Interfaceses;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.Auth
{
    public class AuthService : IAuthService
    {
        private IJwtTokenBuilder _jwtTokenBuilder;
        private IUnitOfWork unitOfWork;

        public AuthService(IJwtTokenBuilder jwtTokenBuilder, IUnitOfWork unitOfWork)
        {
            _jwtTokenBuilder = jwtTokenBuilder;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> BuildTokenAsync(LoginModel loginModel)
        {
            var user = await CheckUserData(loginModel);


            return _jwtTokenBuilder.BuildToken(DateTime.Now, 30, GetUserClaims(user));
        }

        private async Task<User> CheckUserData(LoginModel loginModel)
        {
            var user = await unitOfWork.Users.GetUserAsync(loginModel.Email);
            if(user is null)
            {
                throw new InvalidCredsException($"Cant find user with email: {loginModel.Email}!");
            }
            if (!PasswordHasher.VerifyPassword(loginModel.Password, user.Password))
            {
                throw new InvalidCredsException($"Invalid password!");
            }

            return user;
        }

        private Claim[] GetUserClaims(User user)
        {
            return new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };
        }
    }
}