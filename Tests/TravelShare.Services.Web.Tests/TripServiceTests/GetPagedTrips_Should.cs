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
    public class GetPagedTrips_Should
    {
        [Test]
        public void ReturnCorrectResultOfNotDeleteTripsOrderByDescinngDateAndPaged()
        {
            var firstTrip = new Trip() { Id = 16, IsDeleted = false, Date = new DateTime(1994, 1,1) };
            var secondTrip = new Trip() { Id = 17, IsDeleted = false, Date = new DateTime(1994, 2, 1) };
            var deletedTrip = new Trip() { Id = 15, IsDeleted = true, Date = new DateTime(1994, 1, 1) };
            var list = new List<Trip>()
            {
                firstTrip,
                deletedTrip,
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
            var result = tripService.GetPagedTrips(0, 2).ToList();

            Assert.AreEqual(new List<Trip> { secondTrip, firstTrip }, result);
        }


        [Test]
        public void NotReturnTrips_WhenAllTripsAreDeleted()
        {
            var deletedTrip1 = new Trip() { Id = 16, IsDeleted = true };
            var deletedTrip2 = new Trip() { Id = 17, IsDeleted = true};
            var deletedTrip3 = new Trip() { Id = 15, IsDeleted = true};
            var list = new List<Trip>()
            {
                deletedTrip1,
                deletedTrip3,
                deletedTrip2,
            };

            var mockedDbSet = QueryableDbSetMock.GetQueryableMockDbSet<Trip>(list);

            var mockedContext = new Mock<IApplicationDbContext>();
            mockedContext.Setup(s => s.Set<Trip>())
              .Returns(mockedDbSet);

            var mockRepository = new Mock<IEfDbRepository<Trip>>();
            mockRepository.Setup(x => x.All()).Returns(mockedDbSet.Where(c => c.IsDeleted == false)).Verifiable();

            var tripService = new TripService(mockRepository.Object);

            // Act
            var result = tripService.GetPagedTrips(0, 2).ToList();

            Assert.AreEqual(new List<Trip> { }, result);
        }

        [Test]
        public void ReturnCorrectResultOfNotDeleteTripsOrderByDescinngDateAnd2Paged()
        {
            var firstTrip = new Trip() { Id = 16, IsDeleted = false, Date = new DateTime(1994, 1, 1) };
            var secondTrip = new Trip() { Id = 17, IsDeleted = false, Date = new DateTime(1994, 2, 1) };
            var deletedTrip = new Trip() { Id = 15, IsDeleted = true, Date = new DateTime(1994, 1, 1) };
            var list = new List<Trip>()
            {
                firstTrip,
                deletedTrip,
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
            var result = tripService.GetPagedTrips(1, 1).ToList();

            Assert.AreEqual(new List<Trip> { firstTrip }, result);
        }
    }
}
