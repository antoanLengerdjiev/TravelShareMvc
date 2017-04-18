namespace TravelShare.Web.Controllers.Tests.AccountControllerTests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAuthProviderIsPassed()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AccountController(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullAuthProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Authentication provider cannot be null.";

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AccountController(null));

            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new AccountController(mockAuthProvider.Object));
        }
    }
}
