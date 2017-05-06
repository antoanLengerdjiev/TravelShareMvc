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
    public class SearchNews_Should
    {
        [Test]
        public void CallsAllFromNewRepository_WhenInvoked()
        {
            // Arrange
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);

            // Act
            newsService.SearchNews("bla", "bla", 1, 1);

            // Assert

            mockedNewsRepository.Verify(x => x.All(), Times.Once);
        }

        [Test]
        public void ReturnAllNews_WhenSearchPatternIsNull()
        {
            // Arrange
            var news = new News { Title = "title", Content = "Content" };
            var newsCollection = new List<News> { news, news, news, news };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsCollection.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);

            // Act
            var result = newsService.SearchNews(null, "bla", 1, 5);

            // Assert
            CollectionAssert.AreEqual(newsCollection, result);
        }

        [Test]
        public void ReturnAllNews_WhenSearchPatternIsEmptyString()
        {
            // Arrange
            var news = new News { Title = "title", Content = "Content" };
            var newsCollection = new List<News> { news, news, news, news };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsCollection.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);

            // Act
            var result = newsService.SearchNews(string.Empty, "bla", 1, 5);

            // Assert
            CollectionAssert.AreEqual(newsCollection, result);
        }

        [Test]
        public void ReturnNewsBySearchPatternByDefaultSearchBy()
        {
            // Arrange
            var searchWord = "ti";
            var page = 1;
            var perPage = 5;
            var news = new News { Title = "title", Content = "Content" };
            var news1 = new News { Title = "citle", Content = "Content" };
            var news2 = new News { Title = "tis", Content = "Content" };
            var news3 = new News { Title = "t", Content = "Content" };
            var newsCollection = new List<News> { news, news1, news2, news3 };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsCollection.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);
            var expectedResult = newsCollection.Where(u => u.Title.ToLower().Contains(searchWord.ToLower()))
                .Skip((page - 1) * perPage).Take(perPage);

            // Act
            var result = newsService.SearchNews(searchWord, "bla", page, perPage);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ReturnNewsBySearchPatternAndSearchByTitle()
        {
            // Arrange
            var searchWord = "ti";
            var page = 1;
            var perPage = 5;
            var news = new News { Title = "title", Content = "Content" };
            var news1 = new News { Title = "citle", Content = "Content" };
            var news2 = new News { Title = "tis", Content = "Content" };
            var news3 = new News { Title = "t", Content = "Content" };
            var newsCollection = new List<News> { news, news1, news2, news3 };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsCollection.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);
            var expectedResult = newsCollection.Where(u => u.Title.ToLower().Contains(searchWord.ToLower()))
                .Skip((page - 1) * perPage).Take(perPage);

            // Act
            var result = newsService.SearchNews(searchWord, "title", page, perPage);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ReturnNewsBySearchPatternAndSearchByContent()
        {
            // Arrange
            var searchWord = "Con";
            var page = 1;
            var perPage = 5;
            var news = new News { Title = "title", Content = "Content" };
            var news1 = new News { Title = "citle", Content = "Cotent" };
            var news2 = new News { Title = "tis", Content = "Content" };
            var news3 = new News { Title = "t", Content = "Co3ntent" };
            var newsCollection = new List<News> { news, news1, news2, news3 };
            var mockedNewsRepository = new Mock<IEfDbRepository<News>>();
            mockedNewsRepository.Setup(x => x.All()).Returns(newsCollection.AsQueryable());
            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();

            var newsService = new NewsService(mockedNewsRepository.Object, dbSaveChanges.Object);
            var expectedResult = newsCollection.Where(u => u.Content.ToLower().Contains(searchWord.ToLower()))
                .Skip((page - 1) * perPage).Take(perPage);

            // Act
            var result = newsService.SearchNews(searchWord, "content", page, perPage);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(1, 3)]
        public void ReturnCorrectPagedResult(int page, int perPage)
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
            var expectedResult = newsCollection.Where(u => u.Content.ToLower().Contains(searchWord.ToLower()))
                .Skip((page - 1) * perPage).Take(perPage);

            // Act
            var result = newsService.SearchNews(searchWord, "content", page, perPage);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
            Assert.LessOrEqual(result.Count(), perPage);
        }
    }
}
