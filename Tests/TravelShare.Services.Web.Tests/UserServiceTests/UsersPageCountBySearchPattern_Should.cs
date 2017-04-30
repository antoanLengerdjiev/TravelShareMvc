namespace TravelShare.Services.Web.Tests.UserServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class UsersPageCountBySearchPattern_Should
    {
        [TestCase(null, 1)]
        [TestCase("", 1)]
        [TestCase(null, 2)]
        [TestCase("", 2)]
        public void ReturnAllUserCount_WhenSearchPatternIsNullOrEmptyStringAndPerPageIsLessThanUsersCount(string searchPattern, int perPage)
        {
            // Arrange
            var user1 = new ApplicationUser() { UserName = "pesho" };
            var user2 = new ApplicationUser() { UserName = "esho" };
            var user3 = new ApplicationUser() { UserName = "gosho" };
            var userCollection = new List<ApplicationUser> { user1, user2, user3 };
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.All()).Returns(userCollection.AsQueryable());

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            var expectedResult = (int)Math.Ceiling((double)userCollection.Count / perPage);

            // Act
            var result = service.UsersPageCountBySearchPattern(searchPattern, perPage);

            // Assert
           Assert.AreEqual(expectedResult, result);
        }

        [TestCase(null, 3)]
        [TestCase("", 3)]
        [TestCase(null, 4)]
        [TestCase("", 4)]
        public void ReturnAllUserCount_WhenSearchPatternIsNullOrEmptyStringAndPerPageIsEqualOrBiggerThanUsersCount(string searchPattern, int perPage)
        {
            // Arrange
            var user1 = new ApplicationUser() { UserName = "pesho" };
            var user2 = new ApplicationUser() { UserName = "esho" };
            var user3 = new ApplicationUser() { UserName = "gosho" };
            var userCollection = new List<ApplicationUser> { user1, user2, user3 };
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.All()).Returns(userCollection.AsQueryable());

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = service.UsersPageCountBySearchPattern(searchPattern, perPage);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestCase("esho", 3)]
        [TestCase("sho", 3)]
        [TestCase("esho", 4)]
        [TestCase("sho", 4)]
        public void ReturnFillteredUsersByEmailCount_WhenSearchPatternIsNotNullOrEmptyStringAndPerPageIsEqualOrBiggerThanUsersCount(string searchPattern, int perPage)
        {
            // Arrange
            var user1 = new ApplicationUser() { Email = "pesho", FirstName = "Elon", LastName = "Musk" };
            var user2 = new ApplicationUser() { Email = "esho", FirstName = "Elon", LastName = "Musk" };
            var user3 = new ApplicationUser() { Email = "gosho", FirstName = "Elon", LastName = "Musk" };
            var userCollection = new List<ApplicationUser> { user1, user2, user3 };
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.All()).Returns(userCollection.AsQueryable());

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = service.UsersPageCountBySearchPattern(searchPattern, perPage);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestCase("el", 1)]
        [TestCase("on", 1)]
        [TestCase("mu", 2)]
        public void ReturnFillteredUsersByNameCount_WhenSearchPatternIsNotNullOrEmptyStringAndPerPageIsLessThanUsersCount(string searchPattern, int perPage)
        {
            // Arrange
            var user1 = new ApplicationUser() { Email = "pesho", FirstName = "Elon", LastName = "Musk" };
            var user2 = new ApplicationUser() { Email = "esho", FirstName = "john", LastName = "Musk" };
            var user3 = new ApplicationUser() { Email = "gosho", FirstName = "Elon", LastName = "Musk" };
            var userCollection = new List<ApplicationUser> { user1, user2, user3 };
            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.All()).Returns(userCollection.AsQueryable());

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);
            var userCount = userCollection.Where(u => u.FirstName.ToLower().Contains(searchPattern.ToLower())
                    || u.LastName.ToLower().Contains(searchPattern.ToLower())).Count();
            var expectedResult = (int)Math.Ceiling((double)userCount / perPage);

            // Act
            var result = service.UsersPageCountBySearchPattern(searchPattern, perPage);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
