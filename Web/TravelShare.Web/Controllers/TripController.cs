namespace TravelShare.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Common.Contracts;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
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

        public ActionResult All(int page)
        {
            this.TempData["page"] = page;
            this.TempData["pageCount"] = (this.data.Trips.All().Count() / 5) + 1;
            var trips = this.data.Trips.All().OrderByDescending(x => x.Date).Skip(5 * page).Take(5).MapTo<TripAllModel>().ToList();
            return this.View(trips);
        }

        public ActionResult GetById(int id)
        {
            var trip =this.data.Trips.GetById(id);
            var tripViewModel = AutoMapperConfig.Configuration.CreateMapper().Map<TripDetailedModel>(trip);
            return this.View(tripViewModel);
        }
    }
}