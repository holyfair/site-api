using AutoFixture.Xunit2;
using Models.Auth;
using Models.Exceptions;
using Moq;
using Repository.DatabaseModels;
using Repository.Interfaces;
using Repository.UnitOfWork;
using Services.Auth;
using Services.Helpers;
using Services.Interfaceses;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Tests.Helper;
using Xunit;

namespace Tests.Services.Auth
{
    public class AuthServiceTests
    {
        [Theory, AutoMoqData]
        public async Task BuildTokenAsyncSucessful([Frozen]Mock<IJwtTokenBuilder> jwtTokenBuilderMock, [Frozen]Mock<IUnitOfWork> unitOfWorkMock,
            [Frozen]Mock<IUserRepository> userRepositoryMock, LoginModel loginModel, User user, string token, AuthService sut)
        {
            // Arrange
            var expireIn = 30;
            user.Password = PasswordHasher.EncodePasswordToBase64(loginModel.Password);
            userRepositoryMock.Setup(x => x.GetUserAsync(loginModel.Email)).ReturnsAsync(user);
            unitOfWorkMock.Setup(x => x.Users).Returns(userRepositoryMock.Object);
            jwtTokenBuilderMock.Setup(x => x.BuildToken(It.IsAny<DateTime>(), expireIn, It.IsAny<Claim[]>()))
                .Returns(token);

            // Act
            var result = await sut.BuildTokenAsync(loginModel);

            // Assert
            Assert.Equal(token, result);
            userRepositoryMock.Verify(x => x.GetUserAsync(loginModel.Email), Times.Once);
            unitOfWorkMock.Verify(x => x.Users, Times.Once);
            jwtTokenBuilderMock.Verify(x => x.BuildToken(It.IsAny<DateTime>(), expireIn, It.IsAny<Claim[]>()), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task BuildTokenAsyncThrowInvalidEmailException([Frozen]Mock<IUnitOfWork> unitOfWorkMock,
            [Frozen]Mock<IUserRepository> userRepositoryMock, LoginModel loginModel, AuthService sut)
        {
            // Arrange
            unitOfWorkMock.Setup(x => x.Users).Returns(userRepositoryMock.Object);
            userRepositoryMock.Setup(x => x.GetUserAsync(loginModel.Email)).ReturnsAsync((User)null);

            // Act
            Func<Task> act = async () => await sut.BuildTokenAsync(loginModel);

            // Assert
            Exception result = await Assert.ThrowsAsync<InvalidCredsException>(act);
            Assert.Equal($"Cant find user with email: {loginModel.Email}!", result.Message);
        }

        [Theory, AutoMoqData]
        public async Task BuildTokenAsyncThrowInvalidPasswordException([Frozen]Mock<IUnitOfWork> unitOfWorkMock,
            [Frozen]Mock<IUserRepository> userRepositoryMock, LoginModel loginModel, User user, AuthService sut)
        {
            // Arrange
            var invalidPassword = "password";
            user.Password = PasswordHasher.EncodePasswordToBase64(invalidPassword);
            userRepositoryMock.Setup(x => x.GetUserAsync(loginModel.Email)).ReturnsAsync(user);
            unitOfWorkMock.Setup(x => x.Users).Returns(userRepositoryMock.Object);

            // Act
            Func<Task> act = async () => await sut.BuildTokenAsync(loginModel);

            // Assert
            Exception result = await Assert.ThrowsAsync<InvalidCredsException>(act);
            Assert.Equal("Invalid password!", result.Message);
        }
    }
}
