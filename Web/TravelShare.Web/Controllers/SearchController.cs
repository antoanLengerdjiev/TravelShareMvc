namespace TravelShare.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShare.Web.Infrastructure.Mapping;
    using TravelShare.Web.ViewModels.Trips;
    using ViewModels.Search;

    public class SearchController : BaseController
    {
        private readonly ITripService tripService;

        public SearchController(ITripService tripService)
        {
            Guard.WhenArgument<ITripService>(tripService, "Trip Service cannot ben null.")
                .IsNull()
                .Throw();

            this.tripService = tripService;
        }

        //TODO: Make model
        public ActionResult Index()
        {

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FilteredTrips(SearchTripModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.PartialView("_FilteredTripsPartial");
            }

             List<TripAllModel> trips = AutoMapperConfig.Configuration.CreateMapper().Map<IEnumerable<Trip>, IEnumerable<TripAllModel>>(this.tripService.SearchTrips(model.From, model.To, model.Date).ToList()).ToList();

            return this.PartialView("_FilteredTripsPartial", trips);
        }
    }
}