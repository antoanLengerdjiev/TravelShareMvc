namespace TravelShare.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShareMvc.Providers.Contracts;
    using ViewModels.Trips;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IUserService userService;
        private readonly IAuthenticationProvider authenticationProvider;

        public ProfileController(IUserService userService, IAuthenticationProvider authenticationProvider)
        {
            Guard.WhenArgument<IUserService>(userService, "User Service cannot ben null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, "Authentication provider cannot be null.")
                .IsNull()
                .Throw();

            this.userService = userService;
            this.authenticationProvider = authenticationProvider;
        }

        [HttpGet]
        public ActionResult MyTrips(int page)
        {
            var userId = this.authenticationProvider.CurrentUserId;
            var trips = AutoMapperConfig.Configuration.CreateMapper().Map<IEnumerable<Trip>, IEnumerable<TripAllModel>>(this.userService.MyTrips(userId, page, 5)).ToList();
            var pageCount = this.userService.MyTripsPageCount(userId, 5);
            this.TempData["page"] = page;
            this.TempData["pageCount"] = pageCount;

            return this.View(trips);
        }
    }
}