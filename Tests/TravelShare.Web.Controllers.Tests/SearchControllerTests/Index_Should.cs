namespace TravelShare.Web.Controllers.Tests.SearchControllerTests
{
    using Mappings;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Services.Data.Common.Contracts;

    [TestFixture]
    public class Index_Should
    {
        public void RenderDefaultView()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var controller = new SearchController(mockedTripService.Object, mockMapperProvider.Object);

            // Act && Assert
            controller.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }

    }
}
