using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelShare.Data.Models;
using TravelShare.Services.Data.Common.Contracts;
using TravelShare.Web.Areas.Administration.Controllers;
using TravelShare.Web.Areas.Administration.Models.NewsManagement;
using TravelShare.Web.Mappings;
using TravelShareMvc.Providers.Contracts;

namespace TravelShare.Web.Controllers.Tests.NewsManagementTests
{
    [TestFixture]
    public class SearchNews_Should
    {
        [Test]
        public void ShouldCallSearchNewsMethodOfNewssService()
        {
            // Arrange
            var page = 1;
            var newsPerPage = 5;

            var searchModel = new SearchNewsModel() { SearchWord = "Kon", SearchBy = "title" };
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            newsManagementController.SearchNews(searchModel, new NewsViewModel(), page);

            // Assert
            mockedNewsService.Verify(x => x.SearchNews(searchModel.SearchWord, searchModel.SearchBy, page, newsPerPage), Times.Once);
        }

        [Test]
        public void ShouldCallMapMethodOfMapperProvider()
        {
            // Arrange
            var page = 1;
            var newsPerPage = 5;

            var news = new News { Title = "konnche", Content = "hello .." };
            var newsResult = new List<News> { news, news };

            var searchModel = new SearchNewsModel() { SearchWord = "Kon", SearchBy = "title" };
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(x => x.SearchNews(searchModel.SearchWord, searchModel.SearchBy, page, newsPerPage)).Returns(newsResult);
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();

            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            newsManagementController.SearchNews(searchModel, new NewsViewModel(), page);

            // Assert
            mockedMapperProvider.Verify(x => x.Map<IEnumerable<SingleNewsModel>>(newsResult), Times.Once);
        }

        [Test]
        public void ShouldCallGetSearchNewsPageCountMethodOfNewssService()
        {
            // Arrange
            var page = 1;
            var newsPerPage = 5;

            var searchModel = new SearchNewsModel() { SearchWord = "Kon", SearchBy = "title" };
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act
            newsManagementController.SearchNews(searchModel, new NewsViewModel(), page);

            // Assert
            mockedNewsService.Verify(x => x.GetSearchNewsPageCount(searchModel.SearchWord, searchModel.SearchBy, newsPerPage), Times.Once);
        }

        [Test]
        public void RenderPartialViewNewsPartialWithCorrectModel()
        {
            // Arrange
            var page = 1;
            var newsPerPage = 5;
            var pageCount = 0;
            var news = new News { Title = "konnche", Content = "hello .." };
            var newsResult = new List<News> { news, news };

            var newsModel = new SingleNewsModel { Title = "konnche", Content = "hello .." };
            var mapperResult = new List<SingleNewsModel> { newsModel, newsModel };

            var searchModel = new SearchNewsModel() { SearchWord = "Kon", SearchBy = "title" };
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(x => x.SearchNews(searchModel.SearchWord, searchModel.SearchBy, page, newsPerPage)).Returns(newsResult);
            mockedNewsService.Setup(x => x.GetSearchNewsPageCount(searchModel.SearchWord, searchModel.SearchBy, newsPerPage)).Returns(pageCount);
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(x => x.Map<IEnumerable<SingleNewsModel>>(newsResult)).Returns(mapperResult);

            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);


            // Assert
            newsManagementController.WithCallTo(x => x.SearchNews(searchModel, new NewsViewModel(), page)).ShouldRenderPartialView("NewsPartial").WithModel<NewsViewModel>(model =>
            {
                CollectionAssert.AreEqual(mapperResult, model.News);
                Assert.AreEqual(searchModel, model.SearchModel);
                Assert.AreEqual(page, model.CurrentPage);
                Assert.AreEqual(pageCount, model.Pages);
                Assert.AreEqual(mapperResult.Count, model.NewsCount);
            });
        }

        [Test]
        public void ReturnModelWithPageNumberOne_WhenPageParameterIsNull()
        {
            // Arrange
            int? page = null;
            var newsPerPage = 5;
            var pageCount = 0;
            var news = new News { Title = "konnche", Content = "hello .." };
            var newsResult = new List<News> { news, news };

            var newsModel = new SingleNewsModel { Title = "konnche", Content = "hello .." };
            var mapperResult = new List<SingleNewsModel> { newsModel, newsModel };

            var searchModel = new SearchNewsModel() { SearchWord = "Kon", SearchBy = "title" };
            var mockedNewsService = new Mock<INewsService>();
            mockedNewsService.Setup(x => x.SearchNews(searchModel.SearchWord, searchModel.SearchBy, 1, newsPerPage)).Returns(newsResult);
            mockedNewsService.Setup(x => x.GetSearchNewsPageCount(searchModel.SearchWord, searchModel.SearchBy, newsPerPage)).Returns(pageCount);
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(x => x.Map<IEnumerable<SingleNewsModel>>(newsResult)).Returns(mapperResult);

            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);


            // Assert
            newsManagementController.WithCallTo(x => x.SearchNews(searchModel, new NewsViewModel(), page)).ShouldRenderPartialView("NewsPartial").WithModel<NewsViewModel>(model =>
            {
                Assert.AreEqual(1, model.CurrentPage);
            });
        }
    }
}
