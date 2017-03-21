using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TravelShare.Data.Common;
using TravelShare.Data.Common.Contracts;

namespace TravelShar.Data.Tests.DbRepositoryTests
{
    [TestFixture]
    public class Add
    {
        [Test]
        public void ShouldCallDbContextAddMethod()
        {
            // Arrange 
            var mockedModel = new MockedModel();
            var mockedContext = new Mock<IApplicationDbContext>();
            mockedContext.Setup(x => x.Set<MockedModel>().Add(It.IsAny<MockedModel>())).Verifiable();

            var dbRepository = new DbRepository<MockedModel>(mockedContext.Object);

            // Act
            dbRepository.Add(It.IsAny<MockedModel>());

            // Assert
            mockedContext.Verify(x => x.Set<MockedModel>().Add(It.IsAny<MockedModel>()), Times.Once);
        }
    }
}
