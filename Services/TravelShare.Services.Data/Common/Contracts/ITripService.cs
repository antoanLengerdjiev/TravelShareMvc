﻿using System;
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

        IQueryable<Trip> GetPagedTrips(int page, int number);

        bool IsUserInTrip(string userId,string driverId, IEnumerable<ApplicationUser> passengers);
    }
}