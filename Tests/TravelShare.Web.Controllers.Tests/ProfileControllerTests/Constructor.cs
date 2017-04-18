namespace TravelShare.Web.Controllers.Tests.ProfileControllerTests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserServiceIsPassed()
        {
            // Arrange
            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileController(null, mockAuthProvider.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage__WhenNullUserServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "User Service cannot ben null.";
            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new ProfileController(null, mockAuthProvider.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileController(mockedUserService.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage__WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Authentication provider cannot be null.";
            var mockedUserService = new Mock<IUserService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new ProfileController(mockedUserService.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowNullReferenceException_WhenNullArgumentsArePassed()
        {
            // Arrange, Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileController(null, null));
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new ProfileController(mockedUserService.Object, mockAuthProvider.Object));
        }
    }
}
