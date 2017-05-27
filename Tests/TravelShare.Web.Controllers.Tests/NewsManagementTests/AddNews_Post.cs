namespace TravelShare.Web.Controllers.Tests.NewsManagementTests
{
    using Data.Models;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using Common;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShare.Web.Areas.Administration.Controllers;
    using TravelShare.Web.Areas.Administration.Models.NewsManagement;
    using TravelShare.Web.Mappings;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class AddNews_Post
    {
        [Test]
        public void RenderDefaultView_WhenModelStateIsNotValid()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);
            newsManagementController.ModelState.AddModelError("test", "test");

            // Act & Assert
            newsManagementController.WithCallTo(x => x.AddNews(new NewCreateViewModel())).ShouldRenderDefaultView();
        }

        [Test]
        public void ShouldCallMapMethodFromMapperProvider_WhenModelStateIsValid()
        {
            // Arrange
            var model = new NewCreateViewModel() { Title = "title", Content = "content" };
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            newsManagementController.AddNews(model);

            // Assert
            mockedMapperProvider.Verify(x => x.Map<News>(model), Times.Once);
        }

        [Test]
        public void ShouldCallCreateFromNewsService_WhenModelStateIsValid()
        {
            // Arrange
            var model = new NewCreateViewModel() { Title = "title", Content = "content" };
            var news = new News() { Title = "title", Content = "content" };
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(x => x.Map<News>(model)).Returns(news);
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            newsManagementController.AddNews(model);

            // Assert
            mockedNewsService.Verify(x => x.Create(news), Times.Once);
        }

        [Test]
        public void ShouldInsertItemFromCacheProvider_WhenModelStateIsValid()
        {
            // Arrange

            string newsKey = GlobalConstants.NewsCacheKey;
            var model = new NewCreateViewModel() { Title = "title", Content = "content" };
            var news = new News() { Title = "title", Content = "content" };
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(x => x.Create(news)).Verifiable();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(x => x.Map<News>(model)).Returns(news);
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            newsManagementController.AddNews(model);

            // Assert
            mockedCacheProvider.Verify(x => x.InsertItem(newsKey,news), Times.Once);
        }

        [Test]
        public void ShouldReddirectToIndex_WhenModelStateIstValid()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act & Assert
            newsManagementController.WithCallTo(x => x.AddNews(new NewCreateViewModel())).ShouldRedirectTo<NewsManagementController>(x => x.Index());
        }

    }
}
