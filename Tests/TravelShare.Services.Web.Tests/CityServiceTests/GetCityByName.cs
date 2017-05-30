namespace TravelShare.Services.Web.Tests.CityServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class GetCityByName
    {
        [Test]
        public void ThrowsNullExceptionArgument_WhenParameterIsNull()
        {
            // Arrange
            var mockedCityRepository = new Mock<IEfDbRepository<City>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var cityService = new CityService(mockedCityRepository.Object, dbSaveChanges.Object);

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => cityService.GetCityByName(null));
        }

        [Test]
        public void CallAllMethodFromCityRepository()
        {
            // Arrange
            var mockedCityRepository = new Mock<IEfDbRepository<City>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var cityService = new CityService(mockedCityRepository.Object, dbSaveChanges.Object);

            // Act 
            cityService.GetCityByName("Sofia");

            // Assert
            mockedCityRepository.Verify(x => x.All(), Times.Once);
        }

        [Test]
        public void ReturnInstanceOfCity()
        {
            // Arrange
            var city = new City() { Name = "Sofia" };
            var mockedCityRepository = new Mock<IEfDbRepository<City>>();
            mockedCityRepository.Setup(x => x.All()).Returns(new List<City> { city }.AsQueryable);
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var cityService = new CityService(mockedCityRepository.Object, dbSaveChanges.Object);

            // Act 
            var result = cityService.GetCityByName("Sofia");

            // Assert
            Assert.IsInstanceOf<City>(result);
        }

        [Test]
        public void ReturnsCorrectCity()
        {
            // Arrange
            var city = new City() { Name = "Sofia" };
            var city2 = new City() { Name = "Plovdiv" };
            var city3 = new City() { Name = "Burgas" };
            var mockedCityRepository = new Mock<IEfDbRepository<City>>();
            mockedCityRepository.Setup(x => x.All()).Returns(new List<City> {city3, city,city2 }.AsQueryable);
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var cityService = new CityService(mockedCityRepository.Object, dbSaveChanges.Object);

            // Act 
            var result = cityService.GetCityByName("Sofia");

            // Assert
            Assert.AreSame(city, result);
        }

    }
}
