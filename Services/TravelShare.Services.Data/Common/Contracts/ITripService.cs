using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelShare.Data.Models;
using TravelShare.Services.Data.Common.Models;

namespace TravelShare.Services.Data.Common.Contracts
{
    public interface ITripService
    {
        Trip GetById(int id);

        Trip Create(TripCreationInfo trip);

        void JoinTrip(ApplicationUser user, Trip trip);

        void LeaveTrip(ApplicationUser user, Trip trip);

        void DeleteTrip(string userId, Trip trip);

        int GetPagesCount(int number);

        IEnumerable<Trip> GetPagedTrips(int page, int number);

        bool CanUserJoinTrip(string userId,string driverId, int slots ,IEnumerable<ApplicationUser> passengers);

        IEnumerable<Trip> SearchTrips(string from, string to, DateTime date, int page, int perPage);

        int SearchTripCount(string from, string to, DateTime date, int perPage);

        IEnumerable<Trip> MyTripsAsDriver(string userId, int page, int perPage);

        int MyTripsAsDriverPageCount(string userId, int perPage);

        void AddChat(int tripId, int chatId);
    }
}
