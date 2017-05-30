namespace TravelShare.Web.ViewModels.Trips
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Mappings;
    using Services.Data.Common.Models;

    public class TripCreateModel : IMapFrom<TripCreationInfo>
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