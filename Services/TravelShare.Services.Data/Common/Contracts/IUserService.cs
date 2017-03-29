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
        IEnumerable<Trip> MyTrips(string userId, int page, int number);

        int MyTripsPageCount(string userId, int number);
    }
}
