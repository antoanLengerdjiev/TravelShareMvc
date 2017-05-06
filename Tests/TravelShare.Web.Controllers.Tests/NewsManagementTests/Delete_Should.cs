namespace TravelShare.Web.Controllers.Tests.NewsManagementTests
{
    using Areas.Administration.Models.NewsManagement;
    using Data.Models;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShare.Web.Areas.Administration.Controllers;
    using TravelShare.Web.Mappings;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class Delete_Should
    {
        [Test]
        public void CallsRemoveItemFromCacheProvider()
        {
            // Assert
            var newsId = 5;
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            newsManagementController.Delete(newsId);

            // Assert
            mockedCacheProvider.Verify(x => x.RemoveItem("newsKey"), Times.Once);
        }

        [Test]
        public void CallsGetByIdFromNewsService()
        {
            // Assert
            var newsId = 5;
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            newsManagementController.Delete(newsId);

            // Assert
            mockedNewsService.Verify(x => x.GetById(newsId), Times.Once);
        }

        [Test]
        public void CallsDeleteFromNewsServiceWithResultOfGetById()
        {
            // Assert
            var newsId = 5;
            var news = new News { Title = "konnche", Content = "hello .." , Id = newsId};
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(x => x.GetById(newsId)).Returns(news);
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            newsManagementController.Delete(newsId);

            // Assert
            mockedNewsService.Verify(x => x.Delete(news), Times.Once);
        }


        [Test]
        public void CallsMapMethodFromMapperProviderWithResultOfGetById()
        {
            // Assert
            var newsId = 5;
            var news = new News { Title = "konnche", Content = "hello ..", Id = newsId };
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(x => x.GetById(newsId)).Returns(news);
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            newsManagementController.Delete(newsId);

            // Assert
            mockedMapperProvider.Verify(x => x.Map<SingleNewsModel>(news), Times.Once);
        }

        [Test]
        public void RenderPartialViewDeleteResultWithCorrectModel()
        {
            // Assert
            var newsId = 5;
            var news = new News { Title = "konnche", Content = "hello ..", Id = newsId };
            var newsModel = new SingleNewsModel { Title = "konnche", Content = "hello ..", Id = newsId };

            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(x => x.GetById(newsId)).Returns(news);
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(x => x.Map<SingleNewsModel>(news)).Returns(newsModel);
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act && Assert
            newsManagementController.WithCallTo(x => x.Delete(newsId)).ShouldRenderPartialView("DeleteResult").WithModel<SingleNewsModel>(model =>
            {
                Assert.AreEqual(newsModel.Title, model.Title);
                Assert.AreEqual(newsModel.Content, model.Content);
                Assert.AreEqual(newsModel.Id, model.Id);
            });

        }
    }
}
