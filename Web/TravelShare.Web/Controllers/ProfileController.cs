namespace TravelShare.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using Common;
    using Mappings;
    using TravelShare.Services.Data.Common.Contracts;
    using TravelShareMvc.Providers.Contracts;
    using ViewModels.Trips;

    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IMapperProvider mapper;
        private readonly ITripService tripService;

        public ProfileController(IUserService userService, ITripService tripService, IAuthenticationProvider authenticationProvider, IMapperProvider mapper)
        {
            Guard.WhenArgument<IUserService>(userService, "User Service cannot ben null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<ITripService>(tripService, "Trip Service cannot ben null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, "Authentication provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IMapperProvider>(mapper, "Mapper provider cannot be null.")
               .IsNull()
               .Throw();

            this.userService = userService;
            this.tripService = tripService;
            this.authenticationProvider = authenticationProvider;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult MyTripsAsPassenger(int page)
        {
            var userId = this.authenticationProvider.CurrentUserId;
            var trips =this.mapper.Map<IEnumerable<TripAllModel>>(this.userService.MyTripsAsPassenger(userId, page, GlobalConstants.TripsPerTake)).ToList();
            var pageCount = this.userService.MyTripsAsPassengerPageCount(userId, GlobalConstants.TripsPerTake);
            this.TempData["page"] = page;
            this.TempData["pageCount"] = pageCount;

            return this.View(trips);
        }

        [HttpGet]
        public ActionResult MyTripsAsDriver(int page)
        {
            var userId = this.authenticationProvider.CurrentUserId;
            var trips = this.mapper.Map<IEnumerable<TripAllModel>>(this.tripService.MyTripsAsDriver(userId, page, GlobalConstants.TripsPerTake)).ToList();
            var pageCount = this.tripService.MyTripsAsDriverPageCount(userId, GlobalConstants.TripsPerTake);
            this.TempData["page"] = page;
            this.TempData["pageCount"] = pageCount;

            return this.View(trips);
        }
    }
}