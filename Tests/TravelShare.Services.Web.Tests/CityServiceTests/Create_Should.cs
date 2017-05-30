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
using TravelShare.Services.Data;

namespace TravelShare.Services.Web.Tests.CityServiceTests
{
    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void ThrowsNullExceptionArgument_WhenParameterIsNull()
        {
            // Arrange
            var mockedCityRepository = new Mock<IEfDbRepository<City>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var cityService = new CityService(mockedCityRepository.Object, dbSaveChanges.Object);

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => cityService.Create(null));
        }

        [Test]
        public void CallAddMethodFromCityRepository()
        {
            // Arrange
            var mockedCityRepository = new Mock<IEfDbRepository<City>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var cityService = new CityService(mockedCityRepository.Object, dbSaveChanges.Object);

            // Act
            cityService.Create("sofia");

            // Assert
            mockedCityRepository.Verify(x => x.Add(It.IsAny<City>()), Times.Once);
        }

        [Test]
        public void CallSaveChangesMethodFromDbContextSaveChanges()
        {
            // Arrange
            var mockedCityRepository = new Mock<IEfDbRepository<City>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var cityService = new CityService(mockedCityRepository.Object, dbSaveChanges.Object);

            // Act
            cityService.Create("sofia");

            // Assert
            dbSaveChanges.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void ReturnInstanceOfCity()
        {
            // Arrange
            var mockedCityRepository = new Mock<IEfDbRepository<City>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var cityService = new CityService(mockedCityRepository.Object, dbSaveChanges.Object);

            // Act
            var result = cityService.Create("sofia");

            // Assert
            Assert.IsInstanceOf<City>(result);
        }

        [Test]
        public void ReturnCityWithCorrectName()
        {
            // Arrange
            var cityName = "Sofia";
            var mockedCityRepository = new Mock<IEfDbRepository<City>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var cityService = new CityService(mockedCityRepository.Object, dbSaveChanges.Object);

            // Act
            var result = cityService.Create(cityName);

            // Assert
            Assert.AreEqual(cityName, result.Name);
        }
    }
}
