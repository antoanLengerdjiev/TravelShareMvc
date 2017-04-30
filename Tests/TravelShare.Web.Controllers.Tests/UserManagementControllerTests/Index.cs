namespace TravelShare.Web.Controllers.Tests.UserManagementControllerTests
{
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShare.Web.Areas.Administration.Controllers;
    using TravelShare.Web.Areas.Administration.Models;
    using TravelShare.Web.Mappings;
    using TravelShareMvc.Providers.Contracts;

    [TestFixture]
    public class Index
    {
        [Test]
        public void ShouldReturnDefaultViewWithUsersViewModel()
        {
            // Arrange
            var usersViewModel = new UsersViewModel();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedUsersService = new Mock<IUserService>();

            var userManagementController = new UserManagementController(
                mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUsersService.Object);

            // Act and Assert
            userManagementController.WithCallTo(umc => umc.Index(usersViewModel))
                .ShouldRenderDefaultView()
                .WithModel<UsersViewModel>(model => Assert.AreEqual(usersViewModel, model));
        }
    }
}
