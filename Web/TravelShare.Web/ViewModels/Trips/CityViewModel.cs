using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelShare.Data.Models;
using TravelShare.Web.Mappings;

namespace TravelShare.Web.ViewModels.Trips
{
    public class CityViewModel : IMapFrom<City>
    {
        public string Name { get; set; }
    }
}