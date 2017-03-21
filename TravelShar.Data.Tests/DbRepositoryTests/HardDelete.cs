using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class HardDelete
    {
        [Test]
        public void ShouldCallDbContextRemoveMethod()
        {
            // Arrange 
            var mockedModel = new MockedModel();
            var mockedDbset = new Mock<IDbSet<MockedModel>>();
            var mockedContext = new Mock<IApplicationDbContext>();
           
            mockedContext.Setup(x => x.Set<MockedModel>().Remove(It.IsAny<MockedModel>())).Verifiable();

            var dbRepository = new DbRepository<MockedModel>(mockedContext.Object);
            

            // Act
            dbRepository.HardDelete(It.IsAny<MockedModel>());

            // Assert
            mockedContext.Verify(x => x.Set<MockedModel>().Remove(It.IsAny<MockedModel>()), Times.Once);
        }
    }
}
