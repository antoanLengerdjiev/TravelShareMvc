using System;
using Moq;
using NUnit.Framework;
using TravelShare.Data.Common;
using TravelShare.Data.Models;
using TravelShare.Services.Data;

namespace TravelShare.Services.Web.Tests.UserServiceTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullParameterIsPassed()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new UserService(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUserRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "User repository cannot be null.";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UserService(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }


        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedUserRepository = new Mock<IDbRepository<ApplicationUser>>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new UserService(mockedUserRepository.Object));
        }
    }
}

