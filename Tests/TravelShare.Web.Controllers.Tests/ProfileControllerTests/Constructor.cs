namespace TravelShare.Web.Controllers.Tests.ProfileControllerTests
{
    using System;
    using Mappings;
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
            var mockMapperProvider = new Mock<IMapperProvider>();
            var mockedTripService = new Mock<ITripService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileController(null, mockedTripService.Object,mockAuthProvider.Object, mockMapperProvider.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage__WhenNullUserServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "User Service cannot ben null.";
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var mockedTripService = new Mock<ITripService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new ProfileController(null, mockedTripService.Object, mockAuthProvider.Object, mockMapperProvider.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullTripServiceIsPassed()
        {
            // Arrange
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var mockedUserService = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileController(mockedUserService.Object, null, mockAuthProvider.Object, mockMapperProvider.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage__WhenNullTripServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Trip Service cannot ben null.";
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var mockedUserService = new Mock<IUserService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new ProfileController(mockedUserService.Object, null, mockAuthProvider.Object, mockMapperProvider.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var mockedTripService = new Mock<ITripService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileController(mockedUserService.Object, mockedTripService.Object, null, mockMapperProvider.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage__WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Authentication provider cannot be null.";
            var mockedUserService = new Mock<IUserService>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var mockedTripService = new Mock<ITripService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new ProfileController(mockedUserService.Object, mockedTripService.Object, null, mockMapperProvider.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileController(mockedUserService.Object, mockedTripService.Object, mockAuthProvider.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage__WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Mapper provider cannot be null.";
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new ProfileController(mockedUserService.Object, mockedTripService.Object, mockAuthProvider.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullMapperProviderAndAuthProviderArePassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileController(mockedUserService.Object, mockedTripService.Object, null, null));
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullMapperProviderAndUserServicerArePassed()
        {
            // Arrange
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedTripService = new Mock<ITripService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileController(null, mockedTripService.Object, mockAuthProvider.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAuthProviderAndUserServicerArePassed()
        {
            // Arrange
            var mockMapperProvider = new Mock<IMapperProvider>();
            var mockedTripService = new Mock<ITripService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileController(null, mockedTripService.Object, null, mockMapperProvider.Object));
        }

        [Test]
        public void ShouldThrowNullReferenceException_WhenNullArgumentsArePassed()
        {
            // Arrange, Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ProfileController(null, null, null, null));
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var mockedTripService = new Mock<ITripService>();
            // Act and Assert
            Assert.DoesNotThrow(() =>
                new ProfileController(mockedUserService.Object, mockedTripService.Object, mockAuthProvider.Object, mockMapperProvider.Object));
        }
    }
}
