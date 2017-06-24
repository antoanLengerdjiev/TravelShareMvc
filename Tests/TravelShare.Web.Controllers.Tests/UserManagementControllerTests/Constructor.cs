namespace TravelShare.Web.Controllers.Tests.UserManagementControllerTests
{
    using System;
    using Areas.Administration.Controllers;
    using Common;
    using Mappings;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUserService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new UserManagementController(
                    null,
                mockedMapperProvider.Object,
                mockedUsersService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullAuthenticationProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.AuthenticationProviderNullExceptionMessage;

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUserService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UserManagementController(
                    null,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUsersService = new Mock<IUserService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new UserManagementController(
                    mockedAuthenticationProvider.Object,
                    null,
                    mockedUsersService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.MapperProviderNullExceptionMessage;

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedUsersService = new Mock<IUserService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UserManagementController(
                    mockedAuthenticationProvider.Object,
                    null,
                    mockedUsersService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUsersServiceIsPassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new UserManagementController(
                    mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUsersServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.UserServiceNullExceptionMessage;

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UserManagementController(
                    mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUserService>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new UserManagementController(
                    mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object));
        }
    }
}
