namespace TravelShare.Web.ViewModels.Trips
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Mappings;
    using TravelShare.Data.Models;


    public class TripAllModel : IMapFrom<Trip>
    {

        public int Id { get; set; }

        public CityViewModel FromCity { get; set; }

        public CityViewModel ToCity { get; set; }

        public decimal Money { get; set; }

        public int Slots { get; set; }

        public DateTime Date { get; set; }

        public UserViewModel Driver { get; set; }

        public ICollection<UserViewModel> Passengers { get; set; }
    }
}