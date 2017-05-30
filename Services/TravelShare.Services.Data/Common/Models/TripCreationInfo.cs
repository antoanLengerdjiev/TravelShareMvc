using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelShare.Services.Data.Common.Models
{
    public class TripCreationInfo
    {

        public string From { get; set; }

        public string To { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string DriverId { get; set; }

        public int Slots { get; set; }

        public decimal Money { get; set; }
    }
}
