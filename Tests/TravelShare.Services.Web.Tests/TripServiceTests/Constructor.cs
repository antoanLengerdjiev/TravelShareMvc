﻿namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using System;
    using Common;
    using Data;
    using Data.Common.Contracts;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullTripRepositoryIsPassed()
        {
            // Arrange
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(null, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullTripRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.TripRepositoryNullExceptionMessage;
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new TripService(null, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
         public void ShouldThrowArgumentNullException_WhenNullDbSaveChangesIsPassed()
        {
            // Arrange
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockedCityService = new Mock<ICityService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(mockedTripRepository.Object, null, mockUserRepository.Object, mockedCityService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDbSaveChangesIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.DbContextSaveChangesNullExceptionMessage;
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockedCityService = new Mock<ICityService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new TripService(mockedTripRepository.Object, null, mockUserRepository.Object, mockedCityService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserRepositoryIsPassed()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(mockedTripRepository.Object, mockSaveChanges.Object, null, mockedCityService.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUserRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.UserRepositoryNullExceptionMessage;
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new TripService(mockedTripRepository.Object, mockSaveChanges.Object, null, mockedCityService.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullCityServiceIsPassed()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullNullCityServiceIsPassed()
        {
            // Arrange
            var expectedExMessage = GlobalConstants.CityServiceNullExceptionMessage;
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserRepositoryAndDbSaveChangesArePassed()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockedCityService = new Mock<ICityService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(mockedTripRepository.Object, null, null, mockedCityService.Object));
        }

        public void ShouldThrowArgumentNullException_WhenNullTripRepositoryAndUserRepositoryArePassed()
        {
            // Arrange
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(null, mockSaveChanges.Object, null, mockedCityService.Object));
        }

        public void ShouldThrowArgumentNullException_WhenNullTripRepositoryAndDbSaveChangesArePassed()
        {
            // Arrange
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockedCityService = new Mock<ICityService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(null, null, mockUserRepository.Object,mockedCityService.Object));
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var mockedCityService = new Mock<ICityService>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object));
        }
    }
}
