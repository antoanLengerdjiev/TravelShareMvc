namespace TravelShare.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using Data.Models;
    using Mappings;
    using Services.Data.Common.Contracts;
    using Services.Data.Common.Models;
    using TravelShare.Common;
    using TravelShareMvc.Providers.Contracts;
    using ViewModels.Trips;

    public class TripController : Controller
    {
            private readonly ITripService tripService;
            private readonly IUserService userService;
            private readonly IChatService chatService;
            private readonly IAuthenticationProvider authenticationProvider;
            private readonly IMapperProvider mapper;

            public TripController(ITripService tripService, IUserService userService,IChatService chatService ,IAuthenticationProvider authenticationProvider, IMapperProvider mapper)
            {
                Guard.WhenArgument<ITripService>(tripService, GlobalConstants.TripServiceNullExceptionMessage)
                    .IsNull()
                    .Throw();

                Guard.WhenArgument<IUserService>(userService, GlobalConstants.UserServiceNullExceptionMessage)
                    .IsNull()
                    .Throw();

            Guard.WhenArgument<IChatService>(chatService, GlobalConstants.ChatServiceNullExceptionMessage)
                    .IsNull()
                    .Throw();

            Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, GlobalConstants.AuthenticationProviderNullExceptionMessage)
                    .IsNull()
                    .Throw();

                Guard.WhenArgument<IMapperProvider>(mapper, GlobalConstants.MapperProviderNullExceptionMessage)
                    .IsNull()
                    .Throw();

                this.tripService = tripService;
                this.userService = userService;
                this.chatService = chatService;
                this.authenticationProvider = authenticationProvider;
                this.mapper = mapper;

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
            var tripToBeAdded = this.mapper.Map<TripCreationInfo>(model);
            var trip = this.tripService.Create(tripToBeAdded);
            var chat = this.chatService.Create(trip);
            this.tripService.AddChat(trip.Id, chat.Id);
            return this.RedirectToAction("GetById", new { id = trip.Id });
        }

        [HttpGet]
        public ActionResult All(int page)
        {
            this.TempData["page"] = page;
            this.TempData["pageCount"] = this.tripService.GetPagesCount(GlobalConstants.TripsPerTake);
            List<TripAllModel> trips = this.mapper.Map<IEnumerable<TripAllModel>>(this.tripService.GetPagedTrips(page, GlobalConstants.TripsPerTake)).ToList();

            return this.View(trips);
        }

        public ActionResult GetById(int id)
        {
            var trip = this.tripService.GetById(id);
            var tripViewModel = this.mapper.Map<TripDetailedModel>(trip);
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

        [Authorize]
        public ActionResult Chat(int chatId)
        {
            var oldMessages = this.mapper.Map<IEnumerable<MessageViewModel>>(this.chatService.GetOlderMessages(chatId, GlobalConstants.Zero, GlobalConstants.MessagePerTake));
            return this.PartialView("Chat", oldMessages);
        }

        [Authorize]
        public ActionResult ShowJoinChatButton(int id)
        {
            return this.PartialView("ButtonChatPartial", id);
        }

        [Authorize]
        public ActionResult HideJoinChatButton()
        {
            return this.PartialView("NoChatButton");
        }

        [Authorize]
        public ActionResult GetChatHistory(int id, int skip)
        {
            var oldMessages = this.mapper.Map<IEnumerable<MessageViewModel>>(this.chatService.GetOlderMessages(id, skip, GlobalConstants.MessagePerTake));
            return this.PartialView("ChatHistoryPartial", oldMessages);
        }
    }
}