namespace TravelShare.Web.Controllers.Tests.HomeControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common;
    using Mappings;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;
    using TestStack.FluentMVCTesting;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShareMvc.Providers.Contracts;
    using ViewModels.Home;

    [TestFixture]
    public class Index
    {
        [Test]
        public void ShouldCallGetItemFromCacheProvider_WhenInvoked()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();

            var mockedMapperProvider = new Mock<IMapperProvider>();
            var homeController = new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            homeController.Index();

            // Assert
            mockedCacheProvider.Verify(x => x.GetItem(GlobalConstants.NewsCacheKey), Times.Once);
        }

        [Test]
        public void ShouldCallLastestNewsFromNewsService_WhenCachedDataIsExpired()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            mockedCacheProvider.Setup(x => x.GetItem(GlobalConstants.NewsCacheKey)).Returns(null);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var homeController = new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            homeController.Index();

            // Assert
            mockedNewsService.Verify(x => x.GetLastestNews(5), Times.Once);
        }

        [Test]
        public void ShouldCallLastestNewsFromNewsService_WhenCachedDataIsEmpty()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            mockedCacheProvider.Setup(x => x.GetItem(GlobalConstants.NewsCacheKey)).Returns(new List<News>());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var homeController = new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            homeController.Index();

            // Assert
            mockedNewsService.Verify(x => x.GetLastestNews(5), Times.Once);
        }

        [Test]
        public void ShouldAddItemFromCacheProvider_WhenCachedDataIsExpired()
        {
            // Arrange
            var newsKey = GlobalConstants.NewsCacheKey; 
            var news = new News { Title = "title", Content = "Content" };
            var newsCollection = new List<News> { news, news, news };
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(x => x.GetLastestNews(5)).Returns(newsCollection);
            var mockedCacheProvider = new Mock<ICachingProvider>();
            mockedCacheProvider.Setup(x => x.GetItem(newsKey)).Returns(null);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var homeController = new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            homeController.Index();

            // Assert
            mockedCacheProvider.Verify(x => x.AddItem(newsKey, newsCollection, It.IsAny<DateTime>()), Times.Once);
        }

        [Test]
        public void ShouldCallAddItemFromCacheProvider_WhenCachedDataIsEmpty()
        {
            // Arrange
            var newsKey = GlobalConstants.NewsCacheKey;
            var news = new News { Title = "title", Content = "Content" };
            var newsCollection = new List<News> { news, news, news };
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(x => x.GetLastestNews(5)).Returns(newsCollection);
            var mockedCacheProvider = new Mock<ICachingProvider>();
            mockedCacheProvider.Setup(x => x.GetItem(newsKey)).Returns(new List<News>());
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var homeController = new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            homeController.Index();

            // Assert
            mockedCacheProvider.Verify(x => x.AddItem(newsKey, newsCollection, It.IsAny<DateTime>()), Times.Once);
        }

        [Test]
        public void ShouldNotCallLastestNewsFromNewsService_WhenThereIsCachedData()
        {
            // Arrange
            var newsKey = GlobalConstants.NewsCacheKey;
            var news = new News { Title = "title", Content = "Content" };
            var newsCollection = new List<News> { news, news, news };
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            mockedCacheProvider.Setup(x => x.GetItem(newsKey)).Returns(newsCollection);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var homeController = new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            homeController.Index();

            // Assert
            mockedNewsService.Verify(x => x.GetLastestNews(5), Times.Never);
        }

        [Test]
        public void ShouldNotCallAddItemFromCacheProvider_WhenThereIsCachedData()
        {
            // Arrange
            var newsKey = GlobalConstants.NewsCacheKey;
            var news = new News { Title = "title", Content = "Content" };
            var newsCollection = new List<News> { news, news, news };
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            mockedCacheProvider.Setup(x => x.GetItem(newsKey)).Returns(newsCollection);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var homeController = new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            homeController.Index();

            // Assert
            mockedCacheProvider.Verify(x => x.AddItem(newsKey, newsCollection, It.IsAny<DateTime>()), Times.Never);
        }

        [Test]
        public void ShouldCallMapMethodFromMapperProvider_WhenInvoked()
        {
            // Arrange
            var newsKey = GlobalConstants.NewsCacheKey;
            var news = new News { Title = "title", Content = "Content" };
            var newsCollection = new List<News> { news, news, news };
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            mockedCacheProvider.Setup(x => x.GetItem(newsKey)).Returns(newsCollection);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var homeController = new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            homeController.Index();

            // Assert
            mockedMapperProvider.Verify(x => x.Map<IEnumerable<NewsViewModel>>(newsCollection), Times.Once);
        }

        [Test]
        public void RenderDefaultView()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var homeController = new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act & Assert
            homeController.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void RenderDefaultViewWithCorrectModel()
        {
            // Arrange
            var newsKey = GlobalConstants.NewsCacheKey;
            var news = new News { Title = "title", Content = "Content" };
            var newsCollection = new List<News> { news, news, news };
            var newsViewModel = new NewsViewModel { Title = "title", Content = "Content" };
            var viewModelList = new List<NewsViewModel> { newsViewModel, newsViewModel, newsViewModel };
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            mockedCacheProvider.Setup(x => x.GetItem(newsKey)).Returns(newsCollection);
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(x => x.Map<IEnumerable<NewsViewModel>>(newsCollection)).Returns(viewModelList);
            var homeController = new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act & Assert
            homeController.WithCallTo(x => x.Index()).ShouldRenderDefaultView().WithModel<IEnumerable<NewsViewModel>>(model =>
            {
                CollectionAssert.AreEqual(model, viewModelList);
            });

        }
    }
}
