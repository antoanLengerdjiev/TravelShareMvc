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
    public class Dispose
    {
        [Test]
        public void ShouldCallDbContextDisposeMethod()
        {
            // Arrange 
            var mockedContext = new Mock<IApplicationDbContext>();
            mockedContext.Setup(x => x.Dispose()).Verifiable();

            var dbRepository = new DbRepository<MockedModel>(mockedContext.Object);

            // Act
            dbRepository.Dispose();

            // Assert
            mockedContext.Verify(x => x.Dispose(), Times.Once);
        }
    }
}
