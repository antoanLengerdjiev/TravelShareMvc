namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using TravelShar.Data.Tests.Helper;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using Data;

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

            var mockRepository = new Mock<IDbRepository<Trip>>();
            mockRepository.Setup(x => x.All()).Returns(mockedDbSet.Where(c => c.IsDeleted == false)).Verifiable();

            var tripService = new TripService(mockRepository.Object);

            // Act
            var result = tripService.GetPagesCount(5);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
