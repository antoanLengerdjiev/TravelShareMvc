namespace TravelShare.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bytes2you.Validation;
    using TravelShare.Common;
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
            Guard.WhenArgument<IEfDbRepository<ApplicationUser>>(userRepository, GlobalConstants.UserRepositoryNullExceptionMessage).IsNull().Throw();

            Guard.WhenArgument<IApplicationDbContextSaveChanges>(dbSaveChanges, GlobalConstants.DbContextSaveChangesNullExceptionMessage)
                .IsNull()
                .Throw();

            this.userRepository = userRepository;

            this.dbSaveChanges = dbSaveChanges;
        }

        public IEnumerable<Trip> MyTripsAsPassenger(string userId, int page, int number)
        {
            Guard.WhenArgument<string>(userId, GlobalConstants.UserIdNullExceptionMessage).IsNull().Throw();

            return this.userRepository.GetById(userId).Trips.Where(x => !x.IsDeleted).OrderByDescending(x => x.Date).Skip(page * number).Take(number).ToList();
        }

        public int MyTripsAsPassengerPageCount(string userId, int number)
        {
            Guard.WhenArgument<string>(userId, GlobalConstants.UserIdNullExceptionMessage).IsNull().Throw();

            var tripsCount = this.userRepository.GetById(userId).Trips.Count();
            return tripsCount % number == 0 ? tripsCount / number : (tripsCount / number) + 1;
        }

        public ApplicationUser GetById(string id)
        {
            return this.userRepository.GetById(id);
        }

        public int UsersPageCountBySearchPattern(string searchPattern, int perPage)
        {
            int userCount = this.BuildSearchQuery(searchPattern).Count();

            if (perPage >= userCount)
            {
                return 0;
            }

            return (int)Math.Ceiling((double)userCount / perPage);
        }

        public IEnumerable<ApplicationUser> SearchUsersByUsername(string searchPattern, string sortBy, int page, int perPage)
        {
            var skip = (page - 1) * perPage;
            var users = this.BuildSearchQuery(searchPattern);

            switch (sortBy)
            {
                case "name":
                    users = users.OrderBy(u => u.FirstName);
                    break;
                case "email":
                    users = users.OrderBy(x => x.Email);
                    break;
                default:
                    users = users.OrderBy(x => x.FirstName);
                    break;
            }

            var resultUsers = users
                  .Skip(skip)
                  .Take(perPage)
                  .ToList();

            return resultUsers;
        }

        private IQueryable<ApplicationUser> BuildSearchQuery(string searchWord)
        {
            var users = this.userRepository.All();

            if (!string.IsNullOrEmpty(searchWord))
            {
                users = users.Where(u => u.FirstName.ToLower().Contains(searchWord.ToLower())
                    || u.LastName.ToLower().Contains(searchWord.ToLower())
                    || u.Email.ToLower().Contains(searchWord.ToLower()));
            }

            return users;
        }
    }
}
