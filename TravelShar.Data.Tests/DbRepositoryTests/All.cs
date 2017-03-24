﻿using System;
using System.Collections.Generic;
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
    public class All
    {

        [Test]
        public void AllMethodShouldReturnTheRightResult()
        {
            // Arrange
            var pesho = new MockedModel() { Id = 16, IsDeleted = false };
            var list = new List<MockedModel>()
            {
                new MockedModel() { Id =15,IsDeleted = true},
                pesho


            };
            var mockedDbSet = QueryableDbSetMock.GetQueryableMockDbSet<MockedModel>(list);
            var mockedContext = new Mock<IApplicationDbContext>();
            mockedContext.Setup(s => s.Set<MockedModel>())
              .Returns(mockedDbSet);


            var dbRepository = new DbRepository<MockedModel>(mockedContext.Object);

            // Act
            var all = dbRepository.All();

            // Assert
            Assert.AreEqual(new List<MockedModel> { pesho }, all);

        }
    }
}