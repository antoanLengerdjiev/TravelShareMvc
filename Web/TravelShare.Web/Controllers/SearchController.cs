namespace TravelShare.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using Mappings;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShare.Web.ViewModels.Trips;
    using ViewModels.Search;

    public class SearchController : Controller
    {
        private readonly ITripService tripService;
        private readonly IMapperProvider mapper;

        public SearchController(ITripService tripService, IMapperProvider mapper)
        {
            Guard.WhenArgument<ITripService>(tripService, "Trip Service cannot ben null.")
                .IsNull()
                .Throw();
            Guard.WhenArgument<IMapperProvider>(mapper, "Mapper provider cannot be null.")
               .IsNull()
               .Throw();

            this.tripService = tripService;
            this.mapper = mapper;
        }

        //TODO: Make model
        public ActionResult Index()
        {

            return this.View();
        }

        public ActionResult FilteredTrips(SearchTripModel searchModel, SearchTripResultModel tripsModel, int? page)
        {
            int actualPage = page ?? 0;
            if (!this.ModelState.IsValid)
            {
                return this.PartialView("_FilteredTripsPartial");
            }

            int perPage = 1;
             tripsModel.Trips = this.mapper.Map<IEnumerable<TripAllModel>>(this.tripService.SearchTrips(searchModel.From, searchModel.To, searchModel.Date, actualPage, perPage).ToList()).ToList();
            tripsModel.SearchModel = searchModel;
            tripsModel.CurrentPage = actualPage;
            tripsModel.PagesCount = this.tripService.SearchTripCount(searchModel.From, searchModel.To, searchModel.Date, perPage);
            return this.PartialView("_FilteredTripsPartial", tripsModel);
        }
    }
}