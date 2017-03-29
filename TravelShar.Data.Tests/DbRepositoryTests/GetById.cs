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
    public class GetById
    {
        [TestCase(42)]
        [TestCase(6)]
        public void ShouldCallFindMethodOfDbSet_WhenValidIdIsPassed(int id)
        {
            //Arrange
            var mockedDbSet = new Mock<IDbSet<MockedModel>>();
            mockedDbSet.Setup(s => s.Find(It.IsAny<int>())).Returns(new MockedModel() { IsDeleted = true}).Verifiable();

            var mockedContext = new Mock<IApplicationDbContext>();
            mockedContext.Setup(c => c.Set<MockedModel>())
                .Returns(mockedDbSet.Object);

            var efRepository = new EfDbRepository<MockedModel>(mockedContext.Object);

            // Act
            var entity = efRepository.GetById(id);

            // Assert
            mockedDbSet.Verify(s => s.Find(id), Times.Once);
        }

        [TestCase(15, "Emma")]
        [TestCase(23, "Watson")]
        public void ShouldReturnExactEntity_WhenValidIdIsPassedAndIsNotDeleted(int id, string name)
        {
            //Arrange
            var mockedModel = new MockedModel() { Id = id, Name = name ,IsDeleted = false };

            var mockedDbSet = new Mock<IDbSet<MockedModel>>();
            mockedDbSet.Setup(s => s.Find(id)).Returns(mockedModel);

            var mockedContext = new Mock<IApplicationDbContext>();
            mockedContext.Setup(c => c.Set<MockedModel>())
                .Returns(mockedDbSet.Object);

            var dbRepository = new EfDbRepository<MockedModel>(mockedContext.Object);

            // Act
            var entity = dbRepository.GetById(id);

            // Assert
            Assert.AreSame(mockedModel, entity);
        }

        [TestCase(15, "Emma")]
        [TestCase(23, "Watson")]
        public void ShouldReturnNull_WhenValidIdIsPassedAndIsDeleted(int id, string name)
        {
            //Arrange
            var mockedModel = new MockedModel() { Id = id, Name = name, IsDeleted = true };

            var mockedDbSet = new Mock<IDbSet<MockedModel>>();
            mockedDbSet.Setup(s => s.Find(id)).Returns(mockedModel);

            var mockedContext = new Mock<IApplicationDbContext>();
            mockedContext.Setup(c => c.Set<MockedModel>())
                .Returns(mockedDbSet.Object);

            var dbRepository = new EfDbRepository<MockedModel>(mockedContext.Object);

            // Act
            var entity = dbRepository.GetById(id);

            // Assert
            Assert.AreSame(null, entity);
        }

        public void ShouldReturnNull_WhenIdIsNull()
        {
            //Arrange
            var mockedContext = new Mock<IApplicationDbContext>();
        
            var dbRepository = new EfDbRepository<MockedModel>(mockedContext.Object);

            // Act
            var entity = dbRepository.GetById(null);

            // Assert
            Assert.AreSame(null, entity);
        }
    }
}
