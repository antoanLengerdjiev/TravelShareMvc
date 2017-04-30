namespace TravelShare.Web.Controllers.Tests.SearchControllerTests
{
    using System;
    using System.Collections.Generic;
    using Data.Models;
    using Mappings;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShare.Web.ViewModels.Search;
    using ViewModels.Trips;

    [TestFixture]
    public class FilteredTrips_Should
    {
        [Test]
        public void RenderPartilView_FilteredTripsWhenModelStateIsInvalid()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var searchModel = new SearchTripModel();
            var searchResultModel = new SearchTripResultModel();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var controller = new SearchController(mockedTripService.Object, mockMapperProvider.Object);
            controller.ModelState.AddModelError("test", "test");

            // Act && Assert
            controller.WithCallTo(x => x.FilteredTrips(searchModel, searchResultModel, null)).ShouldRenderPartialView("_FilteredTripsPartial");
        }

        [Test]
        public void CallTripServiceMethodSeachTrips()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var searchModel = new SearchTripModel() { From = "Sofia", To = "Plovdiv", Date = new DateTime(1994,1,1) };
            var searchResultModel = new SearchTripResultModel();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var controller = new SearchController(mockedTripService.Object, mockMapperProvider.Object);

            // Act
            controller.FilteredTrips(searchModel, searchResultModel, null);

            // Assert
            mockedTripService.Verify(x => x.SearchTrips(searchModel.From, searchModel.To, searchModel.Date, 0, 1), Times.Once);
        }

        [Test]
        public void CallTripServiceMethodSeachTripsCount()
        {
            // Arrange
            var searchModel = new SearchTripModel() { From = "Sofia", To = "Plovdiv", Date = new DateTime(1994, 1, 1) };
            var searchResultModel = new SearchTripResultModel();
            var mockedTripService = new Mock<ITripService>();
            var tripResult = new List<Trip>();
            mockedTripService.Setup(x => x.SearchTrips(searchModel.From, searchModel.To, searchModel.Date, 0, 1)).Returns(tripResult).Verifiable();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var controller = new SearchController(mockedTripService.Object, mockMapperProvider.Object);
            // Act
            controller.FilteredTrips(searchModel, searchResultModel, null);

            // Assert
            mockedTripService.Verify(x => x.SearchTripCount(searchModel.From, searchModel.To, searchModel.Date,1), Times.Once);
        }

        [Test]
        public void RenderPartilView_FilteredTripsWhenModelStateIsValid()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var searchModel = new SearchTripModel() { From = "Sofia", To = "Plovdiv", Date = new DateTime(1994, 1, 1) };
            var searchResultModel = new SearchTripResultModel();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var controller = new SearchController(mockedTripService.Object, mockMapperProvider.Object);

            // Act && Assert
            controller.WithCallTo(x => x.FilteredTrips(searchModel, searchResultModel, null)).ShouldRenderPartialView("_FilteredTripsPartial");
        }

        [Test]
        public void RenderPartilViewWithCorrectModel_FilteredTripsWhenModelStateIsValid()
        {
            // Arrange
            var trip = new Trip() { From = "Sofia", To = "Plovdiv", Date = new DateTime(1994, 1, 1) };
            var tripView = new TripAllModel() { From = "Sofia", To = "Plovdiv", Date = new DateTime(1994, 1, 1) };
            var mockedTripService = new Mock<ITripService>();
            var searchModel = new SearchTripModel() { From = "Sofia", To = "Plovdiv", Date = new DateTime(1994, 1, 1) };
            var searchResultModel = new SearchTripResultModel();
            var tripResult = new List<Trip>() {trip};
            var tripViewResult = new List<TripAllModel>() { tripView };
            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(x => x.Map<IEnumerable<TripAllModel>>(It.IsAny<IEnumerable<Trip>>())).Returns(tripViewResult);
            var controller = new SearchController(mockedTripService.Object, mockMapperProvider.Object);
            mockedTripService.Setup(x => x.SearchTrips(searchModel.From, searchModel.To, searchModel.Date, 0, 1)).Returns(tripResult).Verifiable();
            mockedTripService.Setup(x => x.SearchTripCount(searchModel.From, searchModel.To, searchModel.Date, 1)).Returns(2).Verifiable();

             // Act && Assert
             controller.WithCallTo(x => x.FilteredTrips(searchModel, searchResultModel, null)).ShouldRenderPartialView("_FilteredTripsPartial").WithModel<SearchTripResultModel>(model => {

                Assert.AreEqual(model.CurrentPage, 0);
                Assert.AreEqual(model.PagesCount, 2);
                CollectionAssert.AreEqual(model.Trips, tripViewResult);
            });
        }
    }
}
