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

namespace TravelShare.Services.Web.Tests.NewsServiceTests
{
    [TestFixture]
    public class GetLastestNews_Should
    {
        [Test]
        public void CallAllMethodFromNewsRepository_WhenInvoked()
        {
            // Arrange
            var numberOfNews = 3;
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);

            // Act
            newsService.GetLastestNews(numberOfNews);

            // Assert
            mockedNewsRepository.Verify(x => x.All(), Times.Once);
        }

        [Test]
        public void CallReturnAtLeast3News_WhenInvoked()
        {
            // Arrange
            var news = new News { Title = "Title", Content = "Info" };
            var newsList = new List<News> { news, news, news };
            var numberOfNews = 3;
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsList.AsQueryable()).Verifiable();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);

            // Act
            var result = newsService.GetLastestNews(numberOfNews);

            Assert.AreEqual(numberOfNews, result.Count());
        }

        [Test]
        public void CallCorrectResult_WhenInvoked()
        {
            // Arrange
            var news1 = new News { Title = "Title", Content = "Info", CreatedOn = new DateTime(1994, 1, 1) };
            var news2 = new News { Title = "Title", Content = "Info", CreatedOn = new DateTime(1994, 2, 1) };
            var news3 = new News { Title = "Title", Content = "Info", CreatedOn = new DateTime(1994, 3, 1) };
            var newsList = new List<News> { news1, news2, news3 };
            var numberOfNews = 2;
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsList.AsQueryable()).Verifiable();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var expectedResult = new List<News> { news3, news2 };
            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);

            // Act
            var result = newsService.GetLastestNews(numberOfNews);

            CollectionAssert.AreEqual(expectedResult, result);
        }
    }
}
