using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TravelShare.Data.Common;
using TravelShare.Data.Common.Contracts;
using TravelShare.Data.Models;

namespace TravelShar.Data.Tests.ApplicatioDataTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullContextIsPassed()
        {
            // Arrange
            var mockUserRepository = new Mock<IDbRepository<ApplicationUser>>();
            var mockTripRepository = new Mock<IDbRepository<Trip>>();
            var mockNewsRepository = new Mock<IDbRepository<News>>();
            var mockRatingRepository = new Mock<IDbRepository<Rating>>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ApplicationData(null, mockNewsRepository.Object, mockUserRepository.Object, mockTripRepository.Object, mockRatingRepository.Object);
            });
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullContextIsPassed()
        {
            // Arrange
            var expectedExMessage = "Database context cannot be null.";

            var mockUserRepository = new Mock<IDbRepository<ApplicationUser>>();
            var mockTripRepository = new Mock<IDbRepository<Trip>>();
            var mockNewsRepository = new Mock<IDbRepository<News>>();
            var mockRatingRepository = new Mock<IDbRepository<Rating>>();

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() =>
           {
               new ApplicationData(null, mockNewsRepository.Object, mockUserRepository.Object, mockTripRepository.Object, mockRatingRepository.Object);
           });

            // Assert
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullNewsRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "News repository cannot be null.";

            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            var mockUserRepository = new Mock<IDbRepository<ApplicationUser>>();
            var mockTripRepository = new Mock<IDbRepository<Trip>>();
            var mockRatingRepository = new Mock<IDbRepository<Rating>>();

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ApplicationData(mockApplicationDbContext.Object, null, mockUserRepository.Object, mockTripRepository.Object, mockRatingRepository.Object);
            });

            // Assert
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUserRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "User repository cannot be null.";

            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            var mockNewsRepository = new Mock<IDbRepository<News>>();
            var mockTripRepository = new Mock<IDbRepository<Trip>>();
            var mockRatingRepository = new Mock<IDbRepository<Rating>>();

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ApplicationData(mockApplicationDbContext.Object, mockNewsRepository.Object, null, mockTripRepository.Object, mockRatingRepository.Object);
            });

            // Assert
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullTripRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Trip repository cannot be null.";

            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            var mockUserRepository = new Mock<IDbRepository<ApplicationUser>>();
            var mockNewsRepository = new Mock<IDbRepository<News>>();
            var mockRatingRepository = new Mock<IDbRepository<Rating>>();

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ApplicationData(mockApplicationDbContext.Object, mockNewsRepository.Object, mockUserRepository.Object, null, mockRatingRepository.Object);
            });

            // Assert
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullRatingRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Rating repository cannot be null.";

            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            var mockUserRepository = new Mock<IDbRepository<ApplicationUser>>();
            var mockNewsRepository = new Mock<IDbRepository<News>>();
            var mockTripRepository = new Mock<IDbRepository<Trip>>();

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ApplicationData(mockApplicationDbContext.Object, mockNewsRepository.Object, mockUserRepository.Object, mockTripRepository.Object, null);
            });

            // Assert
            StringAssert.Contains(expectedExMessage, exception.Message);
        }


    }
}
