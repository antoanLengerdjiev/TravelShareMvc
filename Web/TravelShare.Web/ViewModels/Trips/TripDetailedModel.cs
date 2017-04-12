namespace TravelShare.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TripDetailedModel : IMapFrom<Trip>
    {
        public int Id { get; set; }

        public string DriverId { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Description { get; set; }

        public decimal Money { get; set; }

        public int Slots { get; set; }

        public DateTime Date { get; set; }

        public UserViewModel Driver { get; set; }

        public ICollection<UserViewModel> Passengers { get; set; }

        public bool IsUserIn { get; set; }
    }
}