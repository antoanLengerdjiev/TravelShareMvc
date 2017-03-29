using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TravelShar.Data.Tests.Helper;
using TravelShare.Data.Common;
using TravelShare.Data.Common.Contracts;

namespace TravelShar.Data.Tests.DbRepositoryTests
{
    [TestFixture]
    public class AllWithDeleted
    {
        [Test]
        public void ShouldReturnDbSet()
        {
            var mockedDbSet = new Mock<IDbSet<MockedModel>>();
            var mockedContext = new Mock<IApplicationDbContext>();
            mockedContext.Setup(s => s.Set<MockedModel>())
                .Returns(mockedDbSet.Object);

            var dbRepository = new EfDbRepository<MockedModel>(mockedContext.Object);

            // Act
            var all = dbRepository.AllWithDeleted();

            // Assert
            Assert.AreSame(mockedDbSet.Object, all);

        }

        
    }
}
