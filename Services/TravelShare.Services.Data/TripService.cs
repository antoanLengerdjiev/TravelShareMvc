namespace TravelShare.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Bytes2you.Validation;
    using TravelShare.Data.Common;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;

    public class TripService : ITripService
    {
        private readonly IEfDbRepository<Trip> tripRepository;

        public TripService(IEfDbRepository<Trip> tripRepository)
        {
            Guard.WhenArgument<IEfDbRepository<Trip>>(tripRepository, "Trip repository cannot be null.")
                .IsNull()
                .Throw();

            this.tripRepository = tripRepository;
        }

        public int GetPagesCount(int number)
        {
            var tripsCount = this.tripRepository.All().Count();
            return tripsCount % number == 0 ? tripsCount / number : (tripsCount / number) + 1;
        }

        public IEnumerable<Trip> GetPagedTrips(int page, int number)
        {
            return this.tripRepository.All().ToList().OrderByDescending(x => x.Date).Skip(page * number).Take(number);
        }

        public bool CanUserJoinTrip(string userId, string driverId, int slots, IEnumerable<ApplicationUser> passengers)
        {
            Guard.WhenArgument<string>(userId, "UserId cannot be null").IsNull().Throw();
            Guard.WhenArgument<string>(driverId, "DriverId cannot be null").IsNull().Throw();
            Guard.WhenArgument<IEnumerable<ApplicationUser>>(passengers, "Passenger cannot be null").IsNull().Throw();

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

        public IEnumerable<Trip> SearchTrips(string from, string to, DateTime date)
        {
            Guard.WhenArgument<string>(from, "From cannot be null").IsNull().Throw();
            Guard.WhenArgument<string>(to, "To cannot be null").IsNull().Throw();

            return this.tripRepository.All().ToList().Where(x => x.Date == date && x.From == from && x.To == to);
        }
    }
}
