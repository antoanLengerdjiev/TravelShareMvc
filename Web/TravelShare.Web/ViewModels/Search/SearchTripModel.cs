using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelShare.Web.ViewModels.Search
{
    public class SearchTripModel
    {
        [Required]
        [MaxLength(100)]
        public string From { get; set; }

        [Required]
        [MaxLength(100)]
        public string To { get; set; }

        [Required(ErrorMessage = "Date is required!")]
        [Display(Name = "Trip Date")]
        public DateTime Date { get; set; }
    }
}