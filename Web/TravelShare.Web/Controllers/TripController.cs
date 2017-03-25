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
           return this.RedirectToAction("Index", "Home");
        }

        public ActionResult All(int page)
        {
            this.TempData["page"] = page;
            this.TempData["pageCount"] =this.tripService.GetPagesCount(5);
            List<TripAllModel> trips = AutoMapperConfig.Configuration.CreateMapper().Map<IEnumerable<Trip>, IEnumerable<TripAllModel>>(this.tripService.GetPagedTrips(page, 5)).ToList();

            return this.View(trips);
        }

        public ActionResult GetById(int id)
        {

            var trip =this.dataProvider.Trips.GetById(id);
            var doesShowJoinButton = false;
            if (this.Request.IsAuthenticated)
            {
                doesShowJoinButton = !this.tripService.IsUserInTrip(this.GetUserId(),trip.DriverId, trip.Passenger.ToList());
            }

            this.ViewData["ShowJoinButton"] = doesShowJoinButton;
            var tripViewModel = AutoMapperConfig.Configuration.CreateMapper().Map<TripDetailedModel>(trip);
            return this.View(tripViewModel);
        }
    }
}