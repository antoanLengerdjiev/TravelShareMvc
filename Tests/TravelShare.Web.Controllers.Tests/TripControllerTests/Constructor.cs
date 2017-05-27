﻿namespace TravelShare.Web.Controllers.Tests.TripControllerTests
{
    using System;
    using Data.Common.Contracts;
    using Mappings;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserServicerIsPassed()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(
               mockedTripService.Object, null, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUserServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "User Service cannot ben null.";
            var mockedTripService = new Mock<ITripService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new TripController(
              mockedTripService.Object, null, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }


        [Test]
        public void ShouldThrowArgumentNullException_WhenNullMessageServicerIsPassed()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(
               mockedTripService.Object, mockedUserService.Object, null, mockAuthProvider.Object, mockMapperProvider.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullMessageServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Message Service cannot ben null.";
            var mockedTripService = new Mock<ITripService>();
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new TripController(
              mockedTripService.Object, mockedUserService.Object, null, mockAuthProvider.Object, mockMapperProvider.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullTripServiceIsPassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(
                    null, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullTripServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = "Trip Service cannot ben null.";
            var mockedUserService = new Mock<IUserService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new TripController(
                    null, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object));

            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullTripServiceAndUserServiceArePassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockMapperProvider = new Mock<IMapperProvider>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(
                    null, null, mockedMessageService.Object ,mockAuthProvider.Object, mockMapperProvider.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserServicerAndAuthProviderArePassed()
        {
            // Arrange
            var mockedMessageService = new Mock<IMessageService>();
            var mockedTripService = new Mock<ITripService>();
            var mockMapperProvider = new Mock<IMapperProvider>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(
               mockedTripService.Object, null, mockedMessageService.Object, null, mockMapperProvider.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(mockedTripService.Object, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage__WhenNullMapperProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Mapper provider cannot be null.";
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
               new TripController(mockedTripService.Object, mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullParatertersArePassed()
        {

            // Arrange, Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(
                    null,
                    null,
                    null,
                    null,
                    null));
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullTripServiceAndAuthProviderArePassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockedMessageService = new Mock<IMessageService>();
            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripController(
                    null, mockedUserService.Object, mockedMessageService.Object, null, null));
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockedTripService = new Mock<ITripService>();
            var mockedMessageService = new Mock<IMessageService>();
            var mockAuthProvider = new Mock<IAuthenticationProvider>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            // Act and Assert
            Assert.DoesNotThrow(() =>
                new TripController(
                    mockedTripService.Object,mockedUserService.Object, mockedMessageService.Object, mockAuthProvider.Object, mockMapperProvider.Object));
        }
    }
}
