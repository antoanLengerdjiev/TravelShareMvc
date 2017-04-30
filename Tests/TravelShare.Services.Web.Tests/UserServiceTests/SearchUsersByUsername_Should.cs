namespace TravelShare.Services.Web.Tests.UserServiceTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data;

    [TestFixture]
    public class SearchUsersByUsername_Should
    {
        [Test]
        public void ShouldReturnAllUsers_WhenNoSearchWordIsProvided()
        {
            // Arrange
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser() { FirstName = "Elon", LastName = "Musk" },
                new ApplicationUser() { FirstName = "Jeff", LastName = "Bezos" }
            };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.All()).Returns(users.AsQueryable());

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = service.SearchUsersByUsername(null, "", 1, 10);

            // Assert
            CollectionAssert.AreEquivalent(users, result);
        }

        [Test]
        public void ShouldReturnCorrectUser_WhenSearchWordIsProvided()
        {
            // Arrange
            var searchWord = "elon";

            var users = new List<ApplicationUser>()
            {
                new ApplicationUser() { FirstName = "Elon", LastName = "Musk", Email="elon" },
                new ApplicationUser() { FirstName = "Jeff", LastName = "Bezos", Email="jeff" }
            };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.All()).Returns(users.AsQueryable());

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = service.SearchUsersByUsername(searchWord, "", 1, 10);

            // Assert
            Assert.AreSame(users[0], result.First());
        }

        [Test]
        public void ShouldReturnCorrectUsersCollection_WhenSearchWordIsProvided()
        {
            // Arrange
            var searchWord = "e";

            var users = new List<ApplicationUser>()
            {
                new ApplicationUser() { FirstName = "Elon", LastName = "Musk", UserName="elon" },
                new ApplicationUser() { FirstName = "Jeff", LastName = "Bezos", UserName="jeff" }
            };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.All()).Returns(users.AsQueryable());

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = service.SearchUsersByUsername(searchWord, "", 1, 10);

            // Assert
            Assert.AreSame(users[0], result.First());
            Assert.AreSame(users[1], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectUsersSortedByEmail_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "e";

            var users = new List<ApplicationUser>()
            {
                new ApplicationUser() { FirstName = "Elon", LastName = "Musk", Email="musk" },
                new ApplicationUser() { FirstName = "Jeff", LastName = "Bezos", Email="jeff" },
                new ApplicationUser() { FirstName = "Charles", LastName = "Xavier", Email="charles" }
            };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.All()).Returns(users.AsQueryable());

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = service.SearchUsersByUsername(searchWord, "email", 1, 10);

            // Assert
            Assert.AreSame(users[2], result.First());
            Assert.AreSame(users[0], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectUsersSortedByName_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "e";

            var users = new List<ApplicationUser>()
            {
                new ApplicationUser() { FirstName = "Elon", LastName = "Musk", UserName="elon" },
                new ApplicationUser() { FirstName = "Jeff", LastName = "Bezos", UserName="jeff" },
                new ApplicationUser() { FirstName = "Charles", LastName = "Xavier", UserName="charles" }
            };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.All()).Returns(users.AsQueryable());

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = service.SearchUsersByUsername(searchWord, "name", 1, 10);

            // Assert
            Assert.AreSame(users[2], result.First());
            Assert.AreSame(users[1], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectUsersPerPage_WhenSearchWordAndPageAreProvided()
        {
            // Arrange
            var searchWord = "e";

            var users = new List<ApplicationUser>()
            {
                new ApplicationUser() { FirstName = "Elon", LastName = "Musk", UserName="elon" },
                new ApplicationUser() { FirstName = "Jeff", LastName = "Bezos", UserName="jeff" }
            };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.All()).Returns(users.AsQueryable());

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);

            // Act
            var result = service.SearchUsersByUsername(searchWord, "", 2, 1);

            // Assert
            Assert.AreSame(users[1], result.First());
        }

        [Test]
        public void ShouldReturnNoUsers_WhenNoUsersAreMatchingTheCriterias()
        {
            // Arrange
            var searchWord = "qwerty";

            var users = new List<ApplicationUser>()
            {
                new ApplicationUser() { FirstName = "Elon", LastName = "Musk", Email="elon" },
                new ApplicationUser() { FirstName = "Jeff", LastName = "Bezos", Email="jeff" }
            };

            var mockUserRepository = new Mock<IEfDbRepository<ApplicationUser>>();
            mockUserRepository.Setup(x => x.All()).Returns(users.AsQueryable());

            var dbSaveChanges = new Mock<IApplicationDbContextSaveChanges>();
            var service = new UserService(mockUserRepository.Object, dbSaveChanges.Object);
            // Act
            var result = service.SearchUsersByUsername(searchWord, "", 1, 10);

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
