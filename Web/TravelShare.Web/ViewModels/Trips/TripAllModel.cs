namespace TravelShare.Web.ViewModels.Trips
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using TravelShare.Data.Models;
    using TravelShare.Web.Infrastructure.Mapping;

    public class TripAllModel : IMapFrom<Trip>
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public decimal Money { get; set; }

        public int Slots { get; set; }

        public DateTime Date { get; set; }

        public UserViewModel Driver { get; set; }

        public ICollection<UserViewModel> Passenger { get; set; }
    }
}