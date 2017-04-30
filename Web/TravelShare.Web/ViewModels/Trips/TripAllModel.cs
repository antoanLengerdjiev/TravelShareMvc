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
        public static Expression<Func<Trip, TripAllModel>> FromTrip
        {
            get
            {
                return trip => new TripAllModel
                {
                    Id = trip.Id,
                    From = trip.From,
                    To = trip.To,
                    Money = trip.Money,
                    Slots = trip.Slots,
                    Date = trip.Date,
                    Driver = new UserViewModel { UserName = trip.Driver.UserName },
                    Passengers = trip.Passengers.Select(x => new UserViewModel { UserName = x.UserName }).ToList()
                };
            }
        }

        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public decimal Money { get; set; }

        public int Slots { get; set; }

        public DateTime Date { get; set; }

        public UserViewModel Driver { get; set; }

        public ICollection<UserViewModel> Passengers { get; set; }
    }
}