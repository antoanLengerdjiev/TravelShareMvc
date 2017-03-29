namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Moq;
    using NUnit.Framework;
    using TravelShar.Data.Tests.Helper;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;

    [TestFixture]
    public class GetPagesCount_Should
    {
        [Test]
        public void ReturnTheRightCountOfPages()
        {

            // Arrange
            var pesho = new Trip() { Id = 16, IsDeleted = false, Date = DateTime.Now };
            var list = new List<Trip>()
            {
                pesho,
                new Trip() { Id = 15, IsDeleted = true, Date = DateTime.Now },
                pesho
            };

            var mockedDbSet = QueryableDbSetMock.GetQueryableMockDbSet<Trip>(list);

            var mockedContext = new Mock<IApplicationDbContext>();
            mockedContext.Setup(s => s.Set<Trip>())
              .Returns(mockedDbSet);

            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            mockRepository.Setup(x => x.All()).Returns(mockedDbSet.Where(c => c.IsDeleted == false)).Verifiable();

            var tripService = new TripService(mockRepository.Object);

            // Act
            var result = tripService.GetPagesCount(2);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void ReturnTheRightCountOfPagesWithAdditionalPage()
        {

            // Arrange
            var firstTrip = new Trip() { Id = 16, IsDeleted = false, Date = new DateTime(1994, 1, 1) };
            var secondTrip = new Trip() { Id = 17, IsDeleted = false, Date = new DateTime(1994, 2, 1) };
            var thirdTrip = new Trip() { Id = 15, IsDeleted = false, Date = new DateTime(1994, 1, 1) };
            var list = new List<Trip>()
            {
                firstTrip,
                thirdTrip,
                secondTrip,
            };

            var mockedDbSet = QueryableDbSetMock.GetQueryableMockDbSet<Trip>(list);

            var mockedContext = new Mock<IApplicationDbContext>();
            mockedContext.Setup(s => s.Set<Trip>())
              .Returns(mockedDbSet);

            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            mockRepository.Setup(x => x.All()).Returns(mockedDbSet.Where(c => c.IsDeleted == false)).Verifiable();

            var tripService = new TripService(mockRepository.Object);

            // Act
            var result = tripService.GetPagesCount(2);

            // Assert
            Assert.AreEqual(2, result);
        }


        [Test]
        public void CallsTripRepositoryMethodAll()
        {

            // Arrange
            var pesho = new Trip() { Id = 16, IsDeleted = false, Date = DateTime.Now };
            var list = new List<Trip>()
            {
                pesho,
                new Trip() { Id = 15, IsDeleted = true, Date = DateTime.Now },
                pesho
            };

            var mockedDbSet = QueryableDbSetMock.GetQueryableMockDbSet<Trip>(list);

            var mockedContext = new Mock<IApplicationDbContext>();
            mockedContext.Setup(s => s.Set<Trip>())
              .Returns(mockedDbSet);

            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            mockRepository.Setup(x => x.All()).Returns(mockedDbSet.Where(c => c.IsDeleted == false)).Verifiable();

            var tripService = new TripService(mockRepository.Object);

            // Act
            var result = tripService.GetPagesCount(5);

            // Assert
            mockRepository.Verify(x => x.All(), Times.Once);
        }
    }
}
