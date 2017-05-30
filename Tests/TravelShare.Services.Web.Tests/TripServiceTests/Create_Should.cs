namespace TravelShare.Services.Web.Tests.TripServiceTests
{
    using Data.Common.Contracts;
    using Data.Common.Models;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void CallGetCityByNameFromCityServiceWithParamaterFromTripCreationPropertyFrom()
        {
            // Arrange
            var orginCityName = "Sofia";
            var destinationCityName = "Plovdiv";

            var orginCity = new City { Name = orginCityName };
            var destinationCity = new City { Name = destinationCityName };

            var tripCreationInfo = new TripCreationInfo { DriverId = "id", From = orginCityName, To = destinationCityName, Date = new System.DateTime(1994, 1, 1), Description = "Kef", Money = 3, Slots = 3 };

            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var mockedCityService = new Mock<ICityService>();
            mockedCityService.Setup(x => x.GetCityByName(orginCityName)).Returns(orginCity);
            mockedCityService.Setup(x => x.GetCityByName(destinationCityName)).Returns(destinationCity);

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.Create(tripCreationInfo);

            // Assert
            mockedCityService.Verify(x => x.GetCityByName(tripCreationInfo.From) , Times.Once);
        }

        [Test]
        public void CallGetCityByNameFromCityServiceWithParamaterFromTripCreationPropertyTo()
        {
            // Arrange
            var orginCityName = "Sofia";
            var destinationCityName = "Plovdiv";

            var orginCity = new City { Name = orginCityName };
            var destinationCity = new City { Name = destinationCityName };

            var tripCreationInfo = new TripCreationInfo { DriverId = "id", From = orginCityName, To = destinationCityName, Date = new System.DateTime(1994, 1, 1), Description = "Kef", Money = 3, Slots = 3 };

            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var mockedCityService = new Mock<ICityService>();
            mockedCityService.Setup(x => x.GetCityByName(orginCityName)).Returns(orginCity);
            mockedCityService.Setup(x => x.GetCityByName(destinationCityName)).Returns(destinationCity);

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.Create(tripCreationInfo);

            // Assert
            mockedCityService.Verify(x => x.GetCityByName(tripCreationInfo.To), Times.Once);
        }

        [Test]
        public void CallCreateMethodFromCityService_WhenCalledMethodGetCityByNameReturnNullCalledWithPropertyFrom()
        {
            // Arrange
            var orginCityName = "Sofia";
            var destinationCityName = "Plovdiv";

            var orginCity = new City { Id = 1, Name = orginCityName };
            var destinationCity = new City { Id = 2, Name = destinationCityName };

            var tripCreationInfo = new TripCreationInfo { DriverId = "id", From = orginCityName, To = destinationCityName, Date = new System.DateTime(1994, 1, 1), Description = "Kef", Money = 3, Slots = 3 };

            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var mockedCityService = new Mock<ICityService>();
            mockedCityService.Setup(x => x.GetCityByName(orginCityName)).Returns((City)null);
            mockedCityService.Setup(x => x.GetCityByName(destinationCityName)).Returns(destinationCity);
            mockedCityService.Setup(x => x.Create(tripCreationInfo.From)).Returns(orginCity);
            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.Create(tripCreationInfo);

            // Assert
            mockedCityService.Verify(x => x.Create(tripCreationInfo.From), Times.Once);
        }

        [Test]
        public void CallCreateMethodFromCityService_WhenCalledMethodGetCityByNameReturnNullCalledWithPropertyTo()
        {
            // Arrange
            var orginCityName = "Sofia";
            var destinationCityName = "Plovdiv";

            var orginCity = new City { Id = 1, Name = orginCityName };
            var destinationCity = new City { Id = 2, Name = destinationCityName };

            var tripCreationInfo = new TripCreationInfo { DriverId = "id", From = orginCityName, To = destinationCityName, Date = new System.DateTime(1994, 1, 1), Description = "Kef", Money = 3, Slots = 3 };

            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var mockedCityService = new Mock<ICityService>();
            mockedCityService.Setup(x => x.GetCityByName(orginCityName)).Returns(orginCity);
            mockedCityService.Setup(x => x.GetCityByName(destinationCityName)).Returns((City)null);
            mockedCityService.Setup(x => x.Create(tripCreationInfo.To)).Returns(destinationCity);
            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.Create(tripCreationInfo);

            // Assert
            mockedCityService.Verify(x => x.Create(tripCreationInfo.To), Times.Once);
        }

        [Test]
        public void CallsAddFromTripRepository()
        {
            // Arrange
            var orginCityName = "Sofia";
            var destinationCityName = "Plovdiv";

            var orginCity = new City { Name = orginCityName };
            var destinationCity = new City { Name = destinationCityName };

            var tripCreationInfo = new TripCreationInfo { DriverId = "id", From = orginCityName, To = destinationCityName, Date = new System.DateTime(1994, 1, 1), Description = "Kef", Money = 3, Slots = 3 };

            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var mockedCityService = new Mock<ICityService>();
            mockedCityService.Setup(x => x.GetCityByName(orginCityName)).Returns(orginCity);
            mockedCityService.Setup(x => x.GetCityByName(destinationCityName)).Returns(destinationCity);

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.Create(tripCreationInfo);

            // Assert
            mockedTripRepository.Verify(x => x.Add(It.IsAny<Trip>()), Times.Once);
        }

        [Test]
        public void CallsSaveChangesFromDbContextSaveChanges()
        {
            // Arrange
            var orginCityName = "Sofia";
            var destinationCityName = "Plovdiv";

            var orginCity = new City { Name = orginCityName };
            var destinationCity = new City { Name = destinationCityName };

            var tripCreationInfo = new TripCreationInfo { DriverId = "id", From = orginCityName, To = destinationCityName, Date = new System.DateTime(1994, 1, 1), Description = "Kef", Money = 3, Slots = 3 };

            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var mockedCityService = new Mock<ICityService>();
            mockedCityService.Setup(x => x.GetCityByName(orginCityName)).Returns(orginCity);
            mockedCityService.Setup(x => x.GetCityByName(destinationCityName)).Returns(destinationCity);

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            tripService.Create(tripCreationInfo);

            // Assert
            mockSaveChanges.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void ReturnCorrectTrip()
        {
            // Arrange
            var orginCityName = "Sofia";
            var destinationCityName = "Plovdiv";

            var orginCity = new City { Id = 1, Name = orginCityName };
            var destinationCity = new City {Id =2, Name = destinationCityName };

            var tripCreationInfo = new TripCreationInfo { DriverId = "id", From = orginCityName, To = destinationCityName, Date = new System.DateTime(1994, 1, 1), Description = "Kef", Money = 3, Slots = 3 };

            var mockedTripRepository = new Mock<IEfDbRepository<Trip>>();
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            var mockSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var mockedCityService = new Mock<ICityService>();
            mockedCityService.Setup(x => x.GetCityByName(orginCityName)).Returns(orginCity);
            mockedCityService.Setup(x => x.GetCityByName(destinationCityName)).Returns(destinationCity);

            var tripService = new TripService(mockedTripRepository.Object, mockSaveChanges.Object, mockUserRepository.Object, mockedCityService.Object);

            // Act
            var result = tripService.Create(tripCreationInfo);

            // Assert
            Assert.AreEqual(tripCreationInfo.DriverId, result.DriverId);
            Assert.AreEqual(tripCreationInfo.Date, result.Date);
            Assert.AreEqual(tripCreationInfo.Description, result.Description);
            Assert.AreEqual(tripCreationInfo.Money, result.Money);
            Assert.AreEqual(tripCreationInfo.Slots, result.Slots);
            Assert.AreEqual(orginCity.Id, result.FromCityId);
            Assert.AreEqual(destinationCity.Id, result.ToCityId);
        }
    }
}
