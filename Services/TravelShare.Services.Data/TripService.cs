namespace TravelShare.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Bytes2you.Validation;
    using Common.Models;
    using TravelShare.Common;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;

    public class TripService : ITripService
    {
        private const string FromNullExceptionMessage = "From cannot be null";
        private const string ToNullExceptionMessage = "To cannot be null";
        private const string DriverIdNullExceptionMessage = "DriverId cannot be null";
        private const string PassengerIdNullExceptionMessage = "Passenger cannot be null";

        private readonly IEfDbRepository<Trip> tripRepository;
        private readonly IEfDbRepository<ApplicationUser> userRepository;
        private readonly IApplicationDbContextSaveChanges dbSaveChanges;
        private readonly ICityService cityService;

        public TripService(IEfDbRepository<Trip> tripRepository, IApplicationDbContextSaveChanges dbSaveChanges, IEfDbRepository<ApplicationUser> userRepository, ICityService cityService)
        {
            Guard.WhenArgument<IEfDbRepository<Trip>>(tripRepository, GlobalConstants.TripRepositoryNullExceptionMessage)
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfDbRepository<ApplicationUser>>(userRepository, GlobalConstants.UserRepositoryNullExceptionMessage)
                .IsNull()
                .Throw();

            Guard.WhenArgument<IApplicationDbContextSaveChanges>(dbSaveChanges, GlobalConstants.DbContextSaveChangesNullExceptionMessage)
                .IsNull()
                .Throw();
            Guard.WhenArgument<ICityService>(cityService, GlobalConstants.CityServiceNullExceptionMessage)
                .IsNull()
                .Throw();

            this.tripRepository = tripRepository;
            this.userRepository = userRepository;
            this.dbSaveChanges = dbSaveChanges;
            this.cityService = cityService;
        }

        public int GetPagesCount(int number)
        {
            var tripsCount = this.tripRepository.All().Count();
            if (tripsCount == 0)
            {
                return tripsCount;
            }

            return tripsCount % number == 0 ? tripsCount / number : (tripsCount / number) + 1;
        }

        public IEnumerable<Trip> GetPagedTrips(int page, int number)
        {
            return this.tripRepository.All().ToList().OrderByDescending(x => x.Date).Skip(page * number).Take(number);
        }

        public bool CanUserJoinTrip(string userId, string driverId, int slots, IEnumerable<ApplicationUser> passengers)
        {
            Guard.WhenArgument<string>(userId, GlobalConstants.UserIdNullExceptionMessage).IsNull().Throw();
            Guard.WhenArgument<string>(driverId, DriverIdNullExceptionMessage).IsNull().Throw();
            Guard.WhenArgument<IEnumerable<ApplicationUser>>(passengers, PassengerIdNullExceptionMessage).IsNull().Throw();

            if (driverId == userId)
            {
                return false;
            }

            if (slots - passengers.Count() <= 0)
            {
                return false;
            }

            foreach (var pass in passengers)
            {
                if (userId == pass.Id)
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerable<Trip> SearchTrips(string from, string to, DateTime date, int page, int perPage)
        {
            Guard.WhenArgument<string>(from, FromNullExceptionMessage).IsNull().Throw();
            Guard.WhenArgument<string>(to, ToNullExceptionMessage).IsNull().Throw();

            return this.tripRepository.All().Where(x => x.Date == date && x.FromCity.Name == from && x.ToCity.Name == to).OrderBy(x => x.CreatedOn).Skip(page * perPage).Take(perPage).ToList();
        }

        public Trip GetById(int id)
        {
            return this.tripRepository.GetById(id);
        }

        public void DeleteTrip(string userId, Trip trip)
        {
            var user = this.userRepository.GetById(userId);
            user.Trips.Remove(trip);
            trip.Passengers.Remove(user);
            this.tripRepository.Delete(trip);
            this.dbSaveChanges.SaveChanges();
        }

        public Trip Create(TripCreationInfo tripInfo)
        {
            var origin = this.cityService.GetCityByName(tripInfo.From);

            if (origin == null)
            {
                origin = this.cityService.Create(tripInfo.From);
            }

            var destination = this.cityService.GetCityByName(tripInfo.To);

            if (destination == null)
            {
                destination = this.cityService.Create(tripInfo.To);
            }

            // TODO : TripFactory
            var trip = new Trip()
            {
                DriverId = tripInfo.DriverId,
                FromCityId = origin.Id,
                ToCityId = destination.Id,
                Description = tripInfo.Description,
                Money = tripInfo.Money,
                Slots = tripInfo.Slots,
                Date = tripInfo.Date

            };
            this.tripRepository.Add(trip);
            this.dbSaveChanges.SaveChanges();

            return trip;
        }

        public void JoinTrip(ApplicationUser user, Trip trip)
        {
            user.Trips.Add(trip);
            trip.Passengers.Add(user);
            this.dbSaveChanges.SaveChanges();
        }

        public void LeaveTrip(ApplicationUser user, Trip trip)
        {
            user.Trips.Remove(trip);
            trip.Passengers.Remove(user);
            this.dbSaveChanges.SaveChanges();
        }

        public int SearchTripCount(string from, string to, DateTime date, int perPage)
        {
            Guard.WhenArgument<string>(from, FromNullExceptionMessage).IsNull().Throw();
            Guard.WhenArgument<string>(to, ToNullExceptionMessage).IsNull().Throw();

            var tripsCount = this.tripRepository.All().Where(x => x.Date == date && x.FromCity.Name == from && x.ToCity.Name == to).Count();

            if (perPage >= tripsCount)
            {
                return 0;
            }

           return (int)Math.Ceiling((double)tripsCount / perPage);
        }

        public IEnumerable<Trip> MyTripsAsDriver(string userId, int page, int perPage)
        {
            Guard.WhenArgument<string>(userId, GlobalConstants.UserIdNullExceptionMessage).IsNull().Throw();

            return this.tripRepository.All().Where(x => x.DriverId == userId).OrderByDescending(x => x.CreatedOn).Skip(page * perPage).Take(perPage).ToList();
        }

        public int MyTripsAsDriverPageCount(string userId, int perPage)
        {
            Guard.WhenArgument<string>(userId, GlobalConstants.UserIdNullExceptionMessage).IsNull().Throw();

            var tripsCount = this.tripRepository.All().Where(x => x.DriverId == userId).Count();

            if (perPage >= tripsCount)
            {
                return 0;
            }

            return (int)Math.Ceiling((double)tripsCount / perPage);
        }

        public void AddChat(int tripId, int chatId)
        {
            var trip = this.tripRepository.GetById(tripId);
            trip.ChatId = chatId;
            this.dbSaveChanges.SaveChanges();
        }
    }
}
