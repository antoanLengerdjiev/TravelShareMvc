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
    public class GetSearchNewsPageCount_Should
    {
        [Test]
        public void CallsAllFromNewRepository_WhenInvoked()
        {
            // Arrange
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);

            // Act
            newsService.GetSearchNewsPageCount("bla", "bla", 5);

            // Assert

            mockedNewsRepository.Verify(x => x.All(), Times.Once);
        }

        [Test]
        public void Return0Pages_WhenPerPageIsBiggerOrEquelThenCount()
        {
            // Arrange
            var news = new News { Title = "title", Content = "Content" };
            var newsCollection = new List<News> { news, news, news, news };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsCollection.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);

            // Act
            var result = newsService.GetSearchNewsPageCount("title", "bla", 5);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void ReturnAllNewsPageCount_WhenSearchPatternIsNull()
        {
            // Arrange
            var perPage = 2;
            var news = new News { Title = "title", Content = "Content" };
            var newsCollection = new List<News> { news, news, news, news };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsCollection.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);
            var expectResult = (int)Math.Ceiling((double)(newsCollection.Count() / perPage));

            // Act
            var result = newsService.GetSearchNewsPageCount(null, "bla", perPage);

            // Assert
            Assert.AreEqual(expectResult, result);
        }

        [Test]
        public void ReturnAllNewsPageCount_WhenSearchPatternIsEmptyString()
        {
            // Arrange
            var perPage = 2;
            var news = new News { Title = "title", Content = "Content" };
            var newsCollection = new List<News> { news, news, news, news };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsCollection.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);
            var expectResult = (int)Math.Ceiling((double)(newsCollection.Count() / perPage));

            // Act
            var result = newsService.GetSearchNewsPageCount(string.Empty, "bla", perPage);

            // Assert
            Assert.AreEqual(expectResult, result);
        }

        [Test]
        public void ReturnPageCountBySearchPatternByDefaultSearchBy()
        {
            // Arrange
            var searchWord = "ti";
            var perPage = 2;
            var news = new News { Title = "title", Content = "Content" };
            var news1 = new News { Title = "citle", Content = "Content" };
            var news2 = new News { Title = "tis", Content = "Content" };
            var news3 = new News { Title = "t", Content = "Content" };
            var newsCollection = new List<News> { news, news1, news2, news3 };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsCollection.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);
            var newsCount = newsCollection.Where(u => u.Title.ToLower().Contains(searchWord.ToLower()))
                .Count();
            var expectedResult = (int)Math.Ceiling((double)(newsCount / perPage));

            // Act
            var result = newsService.GetSearchNewsPageCount(searchWord, "bla", perPage);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ReturnNewsPageCountBySearchPatternAndSearchByTitle()
        {
            // Arrange
            var searchWord = "ti";
            var perPage = 2;
            var news = new News { Title = "title", Content = "Content" };
            var news1 = new News { Title = "citle", Content = "Content" };
            var news2 = new News { Title = "tis", Content = "Content" };
            var news3 = new News { Title = "t", Content = "Content" };
            var newsCollection = new List<News> { news, news1, news2, news3 };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsCollection.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);
            var newsCount = newsCollection.Where(u => u.Title.ToLower().Contains(searchWord.ToLower()))
               .Count();
            var expectedResult = (int)Math.Ceiling((double)(newsCount / perPage));

            // Act
            var result = newsService.GetSearchNewsPageCount(searchWord, "title", perPage);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ReturnCorrectNewsPageCountBySearchPatternAndSearchByContent()
        {
            // Arrange
            var searchWord = "Con";
            var perPage = 2;
            var news = new News { Title = "title", Content = "Content" };
            var news1 = new News { Title = "citle", Content = "Cotent" };
            var news2 = new News { Title = "tis", Content = "Content" };
            var news3 = new News { Title = "t", Content = "Co3ntent" };
            var newsCollection = new List<News> { news, news1, news2, news3 };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsCollection.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);
            var newsCount = newsCollection.Where(u => u.Content.ToLower().Contains(searchWord.ToLower()))
                .Count();
            var expectedResult = (int)Math.Ceiling((double)(newsCount / perPage));

            // Act
            var result = newsService.GetSearchNewsPageCount(searchWord, "content", perPage);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(2)]
        [TestCase(1)]
        [TestCase(3)]
        public void ReturnCorrectPagedResult(int perPage)
        {
            // Arrange
            var searchWord = "Con";
            var news = new News { Title = "title", Content = "Content" };
            var news1 = new News { Title = "citle", Content = "bla" };
            var news2 = new News { Title = "tis", Content = "Content" };
            var news3 = new News { Title = "t", Content = "Co3ntent" };
            var newsCollection = new List<News> { news, news1, news2, news3 };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsCollection.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);
            var newsCount = newsCollection.Where(u => u.Content.ToLower().Contains(searchWord.ToLower()))
                .Count();

            // Act
            var result = newsService.GetSearchNewsPageCount(searchWord, "content",  perPage);
            var expectedResult = (int)Math.Ceiling((double)(newsCount / perPage));

            // Assert
            Assert.AreEqual(result, expectedResult);
        }
    }
}
