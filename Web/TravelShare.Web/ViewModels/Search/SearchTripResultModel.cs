using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelShare.Web.ViewModels.Trips;

namespace TravelShare.Web.ViewModels.Search
{
    public class SearchTripResultModel
    {
        public IEnumerable<TripAllModel> Trips { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public SearchTripModel SearchModel { get; set; }

    }
}