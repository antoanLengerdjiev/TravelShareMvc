﻿using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using TravelShare.Services.Data.Common.Contracts;
using TravelShare.Web.Areas.Administration.Controllers;
using TravelShare.Web.Mappings;
using TravelShareMvc.Providers.Contracts;

namespace TravelShare.Web.Controllers.Tests.NewsManagementTests
{
    [TestFixture]
    public class Index
    {
        [Test]
        public void RenderDefaultView()
        {
            // Arrange
            var mockedNewsService = new Mock<INewsService>();
            var mockedCacheProvider = new Mock<ICachingProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var newsManagementController = new NewsManagementController(mockedNewsService.Object, mockedCacheProvider.Object, mockedMapperProvider.Object);

            // Act & Assert
            newsManagementController.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }
    }
}
