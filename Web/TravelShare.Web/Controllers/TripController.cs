namespace TravelShare.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using Data.Common.Contracts;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Services.Data.Common.Contracts;
    using TravelShareMvc.Providers.Contracts;
    using ViewModels;
    using ViewModels.Trips;

    public class TripController : BaseController
    {
            private readonly ITripService tripService;
            private readonly IUserService userService;
            private readonly IAuthenticationProvider authenticationProvider;

            public TripController(ITripService tripService, IUserService userService, IAuthenticationProvider authenticationProvider)
            {
                Guard.WhenArgument<ITripService>(tripService, "Trip Service cannot ben null.")
                    .IsNull()
                    .Throw();

                Guard.WhenArgument<IUserService>(userService, "User Service cannot ben null.")
                    .IsNull()
                    .Throw();

                Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, "Authentication provider cannot be null.")
                    .IsNull()
                    .Throw();

                this.tripService = tripService;
                this.userService = userService;
                this.authenticationProvider = authenticationProvider;
            }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TripCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.authenticationProvider.CurrentUserId;
            model.DriverId = userId;
            var trip = AutoMapperConfig.Configuration.CreateMapper().Map<Trip>(model);
            this.tripService.Create(trip);
            return this.RedirectToAction("GetById", new { id = trip.Id });
        }

        [HttpGet]
        public ActionResult All(int page)
        {
            this.TempData["page"] = page;
            this.TempData["pageCount"] = this.tripService.GetPagesCount(5);
            List<TripAllModel> trips = AutoMapperConfig.Configuration.CreateMapper().Map<IEnumerable<Trip>, IEnumerable<TripAllModel>>(this.tripService.GetPagedTrips(page, 5)).ToList();

            return this.View(trips);
        }

        public ActionResult GetById(int id)
        {
            var trip = this.tripService.GetById(id);
            var tripViewModel = AutoMapperConfig.Configuration.CreateMapper().Map<TripDetailedModel>(trip);
            if (this.authenticationProvider.IsAuthenticated)
            {
                var userId = this.authenticationProvider.CurrentUserId;
                tripViewModel.IsUserIn = trip.Passengers.Select(x => x.Id).ToList().Contains(userId);
            }

            return this.View(tripViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult JoinTrip(int tripId)
        {
            var userId = this.authenticationProvider.CurrentUserId;
            var user = this.userService.GetById(userId);
            var trip = this.tripService.GetById(tripId);
            if (trip == null || user == null)
            {
                return this.Json(new { notFound = true });
            }

            if (!this.tripService.CanUserJoinTrip(user.Id, trip.DriverId, trip.Slots, trip.Passengers.ToList()))
            {
                return this.Json(new { alreadyIn = true });
            }

            this.tripService.JoinTrip(user, trip);
            var freeSlots = trip.Slots - trip.Passengers.Count < 0 ? 0 : trip.Slots - trip.Passengers.Count;
            return this.Json(new { slots = freeSlots, newPassangerName = user.UserName });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult LeaveTrip(int tripId)
        {
            var userId = this.authenticationProvider.CurrentUserId;
            var user = this.userService.GetById(userId);
            var trip = this.tripService.GetById(tripId);
            if (trip == null || user == null)
            {
                return this.Json(new { notFound = true });
            }

            if (!trip.Passengers.Contains(user))
            {
                return this.Json(new { notIn = true });
            }

            this.tripService.LeaveTrip(user, trip);
            var freeSlots = trip.Slots - trip.Passengers.Count < 0 ? 0 : trip.Slots - trip.Passengers.Count;
            return this.Json(new { slots = freeSlots, removedPassangerName = user.UserName });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTrip(int id)
        {
            var userId = this.authenticationProvider.CurrentUserId;
            var trip = this.tripService.GetById(id);

            if (trip.DriverId != userId)
            {
                return this.RedirectToAction("Forbidden", "Error");
            }

            this.tripService.DeleteTrip(userId, trip);

            return this.RedirectToAction("Create");
        }
    }
}