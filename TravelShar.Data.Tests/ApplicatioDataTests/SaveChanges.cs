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
    public class SaveChanges
    {
        [Test]
        public void ShouldCallSaveChangesMethodOnDbContext()
        {
            // Arrange
            var mockApplicationDbContext = new Mock<IApplicationDbContext>();
            mockApplicationDbContext.Setup(x => x.SaveChanges()).Verifiable();

            var mockUserRepository = new Mock<IDbRepository<ApplicationUser>>();
            var mockTripRepository = new Mock<IDbRepository<Trip>>();
            var mockNewsRepository = new Mock<IDbRepository<News>>();
            var mockRatingRepository = new Mock<IDbRepository<Rating>>();

            var data = new ApplicationData(mockApplicationDbContext.Object, mockNewsRepository.Object, mockUserRepository.Object, mockTripRepository.Object, mockRatingRepository.Object);


            // Act
            data.SaveChanges();

            // Assert
            mockApplicationDbContext.Verify(x => x.SaveChanges(), Times.Once);

        }
    }
}
