namespace TravelShare.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using Common;
    using Mappings;
    using Models;
    using Services.Data.Common.Contracts;
    using TravelShare.Web.Custom.Attributes;
    using TravelShareMvc.Providers.Contracts;

    [Security(Roles = "Administrator", RedirectUrl = "~/error/forbidden")]
    public class UserManagementController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IMapperProvider mapper;

        public UserManagementController(
            IAuthenticationProvider authenticationProvider,
            IMapperProvider mapper,
            IUserService userService)
        {
            Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, GlobalConstants.AuthenticationProviderNullExceptionMessage)
               .IsNull()
               .Throw();

            Guard.WhenArgument<IMapperProvider>(mapper, GlobalConstants.MapperProviderNullExceptionMessage)
               .IsNull()
               .Throw();

            Guard.WhenArgument<IUserService>(userService, GlobalConstants.UserServiceNullExceptionMessage).IsNull().Throw();

            this.authenticationProvider = authenticationProvider;
            this.mapper = mapper;
            this.userService = userService;
        }

        // GET: Administration/UserManagement
        public ActionResult Index(UsersViewModel model)
        {
            return this.View(model);
        }

        public PartialViewResult SearchUsers(SearchModel searchModel, UsersViewModel usersModel, int? page)
        {
            int actualPage = page ?? 1;

            var result = this.userService.SearchUsersByUsername(searchModel.SearchWord,searchModel.SortBy, actualPage, GlobalConstants.UsersPerTake);
            var count = this.userService.UsersPageCountBySearchPattern(searchModel.SearchWord, GlobalConstants.UsersPerTake);

            usersModel.SearchModel = searchModel;

            usersModel.Pages = count;
            usersModel.Page = actualPage;
            usersModel.Users = this.mapper.Map<IEnumerable<SingleUserViewModel>>(result);
            usersModel.UsersCount = result.ToList().Count;
            return this.PartialView("UsersPartial", usersModel);
        }

        // GET: Administration/Users/Id
        public ViewResult UserProfile(UserViewModel model, string id)
        {
            model = this.mapper.Map<UserViewModel>(this.userService.GetById(id));

            model.Role = this.authenticationProvider.GetUserRoles(id).First();

            return this.View(model);
        }

        // POST: Administration/UserManagement/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(UserViewModel model)
        {
            var user = this.userService.GetById(model.Id);

            this.authenticationProvider.ChangeUserRole(user.Id, model.Role);

            this.authenticationProvider.UpdateSecurityStamp(user.Id);

            return this.RedirectToAction("UserProfile", new { id = model.Id });
        }
    }
}