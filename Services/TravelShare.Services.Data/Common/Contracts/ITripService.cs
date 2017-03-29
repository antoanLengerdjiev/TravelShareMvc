using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelShare.Data.Models;

namespace TravelShare.Services.Data.Common.Contracts
{
    public interface ITripService
    {
        int GetPagesCount(int number);

        IEnumerable<Trip> GetPagedTrips(int page, int number);

        bool CanUserJoinTrip(string userId,string driverId, int slots ,IEnumerable<ApplicationUser> passengers);

        IEnumerable<Trip> SearchTrips(string from, string to, DateTime date);
    }
}
