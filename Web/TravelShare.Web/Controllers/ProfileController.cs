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
    using ViewModels.Trips;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IUserService userService;
        public Func<string> GetUserId;

        public ProfileController(IUserService userService)
        {
            Guard.WhenArgument<IUserService>(userService, "User Service cannot ben null.")
                .IsNull()
                .Throw();

            this.userService = userService;
            this.GetUserId = () => this.User.Identity.GetUserId();
        }

        [HttpGet]
        public ActionResult MyTrips(int page)
        {
            var userId = this.GetUserId();
            var trips = AutoMapperConfig.Configuration.CreateMapper().Map<IEnumerable<Trip>, IEnumerable<TripAllModel>>(this.userService.MyTrips(userId, page, 5)).ToList();
            var pageCount = this.userService.MyTripsPageCount(userId, 5);
            this.TempData["page"] = page;
            this.TempData["pageCount"] = pageCount;

            return this.View(trips);
        }
    }
}