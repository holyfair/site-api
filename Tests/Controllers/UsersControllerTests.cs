using Api.Controllers;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Auth;
using Moq;
using Services.Interfaceses;
using System.Threading.Tasks;
using Tests.Helper;
using Xunit;

namespace Tests.Controllers
{
    public class UsersControllerTests
    {
        [Theory, AutoMoqData]
        public async Task CreateUserSuccessful([Frozen]Mock<IUserService> userServiceMock, LoginModel loginModel, UsersController sut)
        {
            // Arrange

            // Act
            var result = await sut.CreateUser(loginModel);

            // Assert
            Assert.IsType<OkResult>(result);
            userServiceMock.Verify(x => x.CreateUserAsync(It.Is<UserDTO>(dto => dto.Email.Equals(loginModel.Email)
                && dto.Password.Equals(loginModel.Password))));
        }
    }
}
