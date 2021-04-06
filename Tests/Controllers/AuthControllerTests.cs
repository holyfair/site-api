using Api.Controllers;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Models.Auth;
using Moq;
using Services.Interfaceses;
using System.Threading.Tasks;
using Tests.Helper;
using Xunit;

namespace Tests.Controllers
{
    public class AuthControllerTests
    {
        [Theory, AutoMoqData]
        public async Task GetTokenAsyncSuccessful([Frozen]Mock<IAuthService> authServiceMock, LoginModel loginModel,
            string token, AuthController sut)
        {
            // Arrange
            authServiceMock.Setup(x => x.BuildTokenAsync(loginModel)).ReturnsAsync(token);

            // Act
            var result = await sut.GetToken(loginModel) as OkObjectResult;

            // Assert
            Assert.Equal(token, result.Value);
        }

        [Theory, AutoMoqData]
        public async Task GetTokenAsyncReturnUnauthorized([Frozen]Mock<IAuthService> authServiceMock, LoginModel loginModel,
            AuthController sut)
        {
            // Arrange
            authServiceMock.Setup(x => x.BuildTokenAsync(loginModel)).ReturnsAsync((string)null);

            // Act
            var result = await sut.GetToken(loginModel);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
