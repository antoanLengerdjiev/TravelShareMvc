namespace TravelShare.Services.Web.Tests.UserServiceTests
{
    using System;
    using Common;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenUserRepositoryIsNull()
        {
            // Arrange
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new UserService(null, dbSaveChanges.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUserRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.UserRepositoryNullExceptionMessage;
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UserService(null,dbSaveChanges.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenDbSaveChangesIsNull()
        {
            // Arrange
            var mockedUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new UserService(mockedUserRepository.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDbSaveChangesIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.DbContextSaveChangesNullExceptionMessage;
            var mockedUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UserService(mockedUserRepository.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }


        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new UserService(mockedUserRepository.Object, dbSaveChanges.Object));
        }
    }
}
