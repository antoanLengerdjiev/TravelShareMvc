namespace TravelShare.Web.ViewModels.Trips
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using Data.Models;
    using Mappings;

    public class TripCreateModel : IMapFrom<Trip>
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

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public string DriverId { get; set; }

        [Range(1, 12, ErrorMessage = "Invalid Range")]
        public int Slots { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Money { get; set; }
    }
}