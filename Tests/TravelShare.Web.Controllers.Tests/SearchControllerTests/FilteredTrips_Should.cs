namespace TravelShare.Web.Controllers.Tests.SearchControllerTests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShare.Web.ViewModels.Search;

    [TestFixture]
    public class FilteredTrips_Should
    {
        [Test]
        public void RenderPartilView_FilteredTripsWhenModelStateIsInvalid()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var searchModel = new SearchTripModel();
            var controller = new SearchController(mockedTripService.Object);
            controller.ModelState.AddModelError("test", "test");

            // Act && Assert
            controller.WithCallTo(x => x.FilteredTrips(searchModel)).ShouldRenderPartialView("_FilteredTripsPartial");
        }

        [Test]
        public void CallTripServiceMethodSeachTrips()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var searchModel = new SearchTripModel() {From = "Sofia", To = "Plovdiv", Date = new DateTime(1994,1,1) };

            var controller = new SearchController(mockedTripService.Object);

            // Act
            controller.FilteredTrips(searchModel);

            // Assert
            mockedTripService.Verify(x => x.SearchTrips(searchModel.From, searchModel.To, searchModel.Date),Times.Once);
        }

        [Test]
        public void RenderPartilView_FilteredTripsWhenModelStateIsValid()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var searchModel = new SearchTripModel() { From = "Sofia", To = "Plovdiv", Date = new DateTime(1994, 1, 1) };

            var controller = new SearchController(mockedTripService.Object);

            // Act && Assert
            controller.WithCallTo(x => x.FilteredTrips(searchModel)).ShouldRenderPartialView("_FilteredTripsPartial");
        }
    }
}
