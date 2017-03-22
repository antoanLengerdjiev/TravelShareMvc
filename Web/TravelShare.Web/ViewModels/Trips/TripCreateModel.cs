namespace TravelShare.Web.ViewModels.Trips
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TripCreateModel : IMapTo<Trip>
    {

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        [RegularExpression("\\d{2}/\\d{2}/\\d{4}", ErrorMessage ="Trip date must be in mm/dd/yyyy format")]
        [Display(Name ="Trip Date")]
        public string DateAsString { get; set; }

        public DateTime Date { get { return DateTime.ParseExact(this.DateAsString, "MM/dd/yyyy", CultureInfo.InvariantCulture); } }

        [Required]
        public string Description { get; set; }

        public string DriverId { get; set; }

        [Range(1, 12,ErrorMessage ="Invalid Range")]
        public int Slots { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Money { get; set; }
    }
}