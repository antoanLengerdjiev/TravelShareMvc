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
    using ViewModels;
    using ViewModels.Trips;

    public class TripController : BaseController
    {
            private readonly IApplicationData dataProvider;
            private readonly ITripService tripService;
            public Func<string> GetUserId;

            public TripController(IApplicationData dataProvider, ITripService tripService)
            {
                Guard.WhenArgument<IApplicationData>(dataProvider, "Data provider cannot be null.")
                    .IsNull()
                    .Throw();

                Guard.WhenArgument<ITripService>(tripService, "Trip Service cannot ben null.")
                    .IsNull()
                    .Throw();

                this.dataProvider = dataProvider;
                this.tripService = tripService;
                this.GetUserId = () => this.User.Identity.GetUserId();
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

            var userId = this.GetUserId();
            model.DriverId = userId;
            var trip = AutoMapperConfig.Configuration.CreateMapper().Map<Trip>(model);
            this.dataProvider.Trips.Add(trip);
            var user = this.dataProvider.Users.GetById(userId);
            user.Trips.Add(trip);
            this.dataProvider.SaveChanges();
           return this.RedirectToAction("GetById", new {id = trip.Id });
        }

        [HttpGet]
        public ActionResult All(int page)
        {
            this.TempData["page"] = page;
            this.TempData["pageCount"] =this.tripService.GetPagesCount(5);
            List<TripAllModel> trips = AutoMapperConfig.Configuration.CreateMapper().Map<IEnumerable<Trip>, IEnumerable<TripAllModel>>(this.tripService.GetPagedTrips(page, 5)).ToList();

            return this.View(trips);
        }

        public ActionResult GetById(int id)
        {
            var trip = this.dataProvider.Trips.GetById(id);
            var doesShowJoinButton = false;
            if (this.Request.IsAuthenticated)
            {
                doesShowJoinButton = this.tripService.CanUserJoinTrip(this.GetUserId(), trip.DriverId,trip.Slots, trip.Passengers.ToList());
            }

            this.ViewData["ShowJoinButton"] = doesShowJoinButton;
            var tripViewModel = AutoMapperConfig.Configuration.CreateMapper().Map<TripDetailedModel>(trip);
            return this.View(tripViewModel);
        }

        [Authorize]
        [HttpPost]
        public JsonResult JoinTrip(int tripId)
        {
            var user = this.dataProvider.Users.GetById(this.GetUserId());
            var trip = this.dataProvider.Trips.GetById(tripId);
            if (trip == null || user == null)
            {
                return this.Json(new { notFound = true });
            }

            if (!this.tripService.CanUserJoinTrip(user.Id,trip.DriverId, trip.Slots,trip.Passengers.ToList()))
            {
                return this.Json(new { alreadyIn = true });
            }

            user.Trips.Add(trip);
            trip.Passengers.Add(user);
            this.dataProvider.SaveChanges();
            var freeSlots = trip.Slots - trip.Passengers.Count < 0 ? 0 : trip.Slots - trip.Passengers.Count;
            return this.Json(new { slots = freeSlots, newPassangerName = user.UserName });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTrip(int id)
        {
            var user = this.dataProvider.Users.GetById(this.GetUserId());
            var trip = this.dataProvider.Trips.GetById(id);

           if (trip.DriverId != user.Id)
            {
                return this.RedirectToAction("Forbidden", "Error");
            }

            user.Trips.Remove(trip);
            trip.Passengers.Remove(user);
            this.dataProvider.Trips.Delete(trip);
            this.dataProvider.SaveChanges();

            return this.RedirectToAction("Create");
        }
    }
}