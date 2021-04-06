using AutoFixture.Xunit2;
using Models;
using Models.Exceptions;
using Moq;
using Repository.DatabaseModels;
using Repository.Interfaces;
using Repository.UnitOfWork;
using Services;
using Services.Helpers;
using System;
using System.Threading.Tasks;
using Tests.Helper;
using Xunit;

namespace Tests.Services
{
    public class UserServiceTests
    {
        [Theory, AutoMoqData]
        public async Task CreateUserAsyncSuccessful([Frozen]Mock<IUnitOfWork> unitOfWorkMock,
            [Frozen]Mock<IUserRepository> userRepositoryMock, UserDTO model, UserService sut)
        {
            // Arrange
            var encodedPassword = PasswordHasher.EncodePasswordToBase64(model.Password);
            userRepositoryMock.Setup(x => x.GetUserAsync(model.Email)).ReturnsAsync((User) null);
            unitOfWorkMock.Setup(x => x.Users).Returns(userRepositoryMock.Object);

            // Act
            await sut.CreateUserAsync(model);

            // Assert
            userRepositoryMock.Verify(x => x.GetUserAsync(model.Email), Times.Once);
            userRepositoryMock.Verify(x => x.CreateUserAsync(It.Is<User>(user => user.Email.Equals(model.Email))), Times.Once);
            unitOfWorkMock.Verify(x => x.Users, Times.Exactly(2));
            unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Theory, AutoMoqData]
        public async Task CreateUserAsyncBadRequestException([Frozen]Mock<IUnitOfWork> unitOfWorkMock,
            [Frozen]Mock<IUserRepository> userRepositoryMock, UserDTO model, User userRecord, UserService sut)
        {
            // Arrange
            userRepositoryMock.Setup(x => x.GetUserAsync(model.Email)).ReturnsAsync(userRecord);
            unitOfWorkMock.Setup(x => x.Users).Returns(userRepositoryMock.Object);

            // Act
            Func<Task> act = async () => await sut.CreateUserAsync(model);

            // Assert
            Exception result = await Assert.ThrowsAsync<BadRequestException>(act);
            Assert.Equal("User with this email alredy exists", result.Message);
            userRepositoryMock.Verify(x => x.GetUserAsync(model.Email), Times.Once);;
            unitOfWorkMock.Verify(x => x.Users, Times.Once);
        }
    }
}
