namespace TravelShare.Web.Controllers.Tests.ProfileControllerTests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Services.Data.Common.Contracts;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserServiceIsPassed()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileController(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage__WhenNullUserServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "User Service cannot ben null.";


            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new ProfileController(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new ProfileController(mockedUserService.Object));
        }
    }
}
