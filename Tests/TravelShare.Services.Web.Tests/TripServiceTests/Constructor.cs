namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using System;
    using Data;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;

    [TestFixture]
    public class Constructor
    {

        public void ShouldThrowArgumentNullException_WhenNullTripRepositoryIsPassed()
        {
            // Arrange
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(null, mockSaveChanges.Object, mockUserRepository.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullTripRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Trip repository cannot be null.";
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new TripService(null, mockSaveChanges.Object, mockUserRepository.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
         public void ShouldThrowArgumentNullException_WhenNullDbSaveChangesIsPassed()
        {
            // Arrange
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(mockedTripRepository.Object, null, mockUserRepository.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDbSaveChangesIsPassed()
        {
            // Arrange
            var expectedExMessage = "DbContext cannot be null.";
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new TripService(mockedTripRepository.Object, null, mockUserRepository.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserRepositoryIsPassed()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(mockedTripRepository.Object, mockSaveChanges.Object, null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUserRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "User repository cannot be null.";
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new TripService(mockedTripRepository.Object, mockSaveChanges.Object,null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUserRepositoryAndDbSaveChangesArePassed()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(mockedTripRepository.Object, null, null));
        }

        public void ShouldThrowArgumentNullException_WhenNullTripRepositoryAndUserRepositoryArePassed()
        {
            // Arrange
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(null, mockSaveChanges.Object, null));
        }

        public void ShouldThrowArgumentNullException_WhenNullTripRepositoryAndDbSaveChangesArePassed()
        {
            // Arrange
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TripService(null, null, mockUserRepository.Object));
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object));
        }
    }
}
