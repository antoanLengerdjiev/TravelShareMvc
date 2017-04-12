namespace TravelShare.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bytes2you.Validation;
    using TravelShare.Data.Common;
    using TravelShare.Data.Common.Contracts;
    using TravelShare.Data.Models;
    using TravelShare.Services.Data.Common.Contracts;

    public class UserService : IUserService
    {
        private readonly IEfDbRepository<ApplicationUser> userRepository;
        private readonly IApplicationDbContextSaveChanges dbSaveChanges;

        public UserService(IEfDbRepository<ApplicationUser> userRepository, IApplicationDbContextSaveChanges dbSaveChanges)
        {
            Guard.WhenArgument<IEfDbRepository<ApplicationUser>>(userRepository, "User repository cannot be null.").IsNull().Throw();

            Guard.WhenArgument<IApplicationDbContextSaveChanges>(dbSaveChanges, "DbContext cannot be null.")
                .IsNull()
                .Throw();

            this.userRepository = userRepository;

            this.dbSaveChanges = dbSaveChanges;
        }

        public IEnumerable<Trip> MyTrips(string userId, int page, int number)
        {
            Guard.WhenArgument<string>(userId, "User Id cannot be null").IsNull().Throw();

            return this.userRepository.GetById(userId).Trips.Where(x => !x.IsDeleted).OrderByDescending(x => x.Date).Skip(page * number).Take(number).ToList();
        }

        public int MyTripsPageCount(string userId, int number)
        {
            Guard.WhenArgument<string>(userId, "User Id cannot be null").IsNull().Throw();

            var tripsCount = this.userRepository.GetById(userId).Trips.Count();
            return tripsCount % number == 0 ? tripsCount / number : (tripsCount / number) + 1;
        }

        public ApplicationUser GetById(string id)
        {
            return this.userRepository.GetById(id);
        }
    }
}
