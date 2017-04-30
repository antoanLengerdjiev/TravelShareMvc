using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TravelShare.Data.Common;
using TravelShare.Data.Common.Contracts;
using TravelShare.Data.Models;
using TravelShare.Services.Data;

namespace TravelShare.Services.Web.Tests.UserServiceTests
{
    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void CallsUserRepositoryMethodGetById()
        {
            // Arrange
            var mockedUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var userService = new UserService(mockedUserRepository.Object, dbSaveChanges.Object);

            // Act
            userService.GetById(It.IsAny<string>());

            // Assert
            mockedUserRepository.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallsUserRepositoryMethodGetByIdWithTheSameUserId()
        {
            // Arrange
            var mockedUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var userId = "USERID";
            var userService = new UserService(mockedUserRepository.Object, dbSaveChanges.Object);

            // Act
            userService.GetById(userId);

            // Assert
            mockedUserRepository.Verify(x => x.GetById(userId), Times.Once);
        }

        [Test]
        public void ReturnCorrectResult()
        {
            // Arrange
            var userId = "USERID";
            var appUser = new ApplicationUser() { Id = userId };
            var mockedUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockedUserRepository.Setup(x => x.GetById(userId)).Returns(appUser);
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var userService = new UserService(mockedUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = userService.GetById(userId);

            // Assert
            Assert.AreSame(appUser, result);

        }

        [Test]
        public void ReturnInstanceOfApplicationUser()
        {
            // Arrange
            var userId = "USERID";
            var appUser = new ApplicationUser() { Id = userId };
            var mockedUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockedUserRepository.Setup(x => x.GetById(userId)).Returns(appUser);
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var userService = new UserService(mockedUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = userService.GetById(userId);

            // Assert
            Assert.IsInstanceOf<ApplicationUser>(result);

        }
    }
}
