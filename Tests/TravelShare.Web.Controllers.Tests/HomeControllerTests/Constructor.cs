namespace TravelShare.Web.Controllers.Tests.HomeControllerTests
{
    using System;
    using Common;
    using Data.Common.Contracts;
    using Mappings;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Common.Contracts;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ThrowsNullArgumentException_WhenNewsServiceIsNull()
        {
            // Arrange
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(null, mockedCacheProvider.Object, mockedMapperProvider.Object));
        }

        [Test]
        public void ThrowsNullArgumentExceptionWithCorrectMessage_WhenNewsServiceIsNull()
        {
            // Arrange
            var expectedMessage = GlobalConstants.NewsServiceNullExceptionMessage;
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();

            // Act && Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new HomeController(null, mockedCacheProvider.Object, mockedMapperProvider.Object));

            StringAssert.Contains(expectedMessage, exception.Message);
        }


        [Test]
        public void ThrowsNullArgumentException_WhenCacheProviderIsNull()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedMapperProvider = new Mock<IMapperProvider>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(mockedNewsService.Object, null, mockedMapperProvider.Object));
        }

        [Test]
        public void ThrowsNullArgumentExceptionWithCorrectMessage_WhenCacheProviderIsNull()
        {
            // Arrange
            var expectedMessage = GlobalConstants.CacheProviderNullExceptionMessage;
            var mockedNewsService = new Mock<INewsService>();
            var mockedMapperProvider = new Mock<IMapperProvider>();

            // Act && Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new HomeController(mockedNewsService.Object, null, mockedMapperProvider.Object));

            StringAssert.Contains(expectedMessage, exception.Message);
        }

        [Test]
        public void ThrowsNullArgumentException_WhenMapperProviderIsNull()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, null));
        }

        [Test]
        public void ThrowsNullArgumentExceptionWithCorrectMessage_WhenMapperProviderIsNull()
        {
            // Arrange
            var expectedMessage = GlobalConstants.MapperProviderNullExceptionMessage;
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();

            // Act && Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, null));

            StringAssert.Contains(expectedMessage, exception.Message);
        }

        [Test]
        public void DoestNotThorw_WhenParametersAreValid()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();

            // Act && Assert
            Assert.DoesNotThrow(() => new HomeController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object));
        }

    }
}
