namespace TravelShare.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using TravelShare.Data.Common.Contracts;
    using Infrastructure.Mapping;
    using ViewModels.Trips;

    public class TripController : BaseController
    {
        private readonly IApplicationData data;
        public Func<string> GetUserId;

        public TripController(IApplicationData data)
        {
            this.data = data;
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

            model.DriverId = this.GetUserId();
            var trip = AutoMapperConfig.Configuration.CreateMapper().Map<Trip>(model);
            this.data.Trips.Add(trip);
            this.data.SaveChanges();
           return this.RedirectToAction("Index", "Home");
        }
    }
}