using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelShare.Data.Models;

namespace TravelShare.Services.Data.Common.Contracts
{
    public interface IUserService
    {
        ApplicationUser GetById(string id);

        IEnumerable<Trip> MyTripsAsPassenger(string userId, int page, int number);

        int MyTripsAsPassengerPageCount(string userId, int number);

        IEnumerable<ApplicationUser> SearchUsersByUsername(string searchPattern, string sortBy, int page, int perPage);

        int UsersPageCountBySearchPattern(string searchPattern, int perPage);
    }
}
