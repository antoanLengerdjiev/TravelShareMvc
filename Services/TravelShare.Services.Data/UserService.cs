namespace TravelShare.Services.Data
{
    using System;
    using System.Linq;
    using Bytes2you.Validation;
    using TravelShare.Data.Common;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;

    public class UserService : IUserService
    {
        private readonly IDbRepository<ApplicationUser> userRepository;

        public UserService(IDbRepository<ApplicationUser> userRepository)
        {
            Guard.WhenArgument<IDbRepository<ApplicationUser>>(userRepository, "User repository cannot be null.").IsNull().Throw();
            this.userRepository = userRepository;
        }

        public IQueryable<Trip> MyTrips(string userId, int page, int number)
        {
            Guard.WhenArgument<string>(userId, "User Id cannot be null").IsNull().Throw();

            return this.userRepository.GetById(userId).Trips.Where(x => !x.IsDeleted).OrderByDescending(x => x.Date).Skip(page * number).Take(number).AsQueryable();
        }

        public int MyTripsPageCount(string userId, int number)
        {
            Guard.WhenArgument<string>(userId, "User Id cannot be null").IsNull().Throw();

            var tripsCount = this.userRepository.GetById(userId).Trips.Count();
            return tripsCount % number == 0 ? tripsCount / number : (tripsCount / number) + 1;
        }
    }
}
