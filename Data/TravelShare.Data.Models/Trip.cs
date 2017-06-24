namespace TravelShare.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TravelShare.Data.Models.Base;

    public class Trip : BaseModel<int>
    {
        public Trip()
        {
            this.Passengers = new HashSet<ApplicationUser>();
        }

        [Required]
        [ForeignKey("FromCity")]
        public int FromCityId { get; set; }

        public virtual City FromCity { get; set; }

        [Required]
        [ForeignKey("ToCity")]
        public int ToCityId { get; set; }

        public virtual City ToCity { get; set; }

        [Required]
        public string DriverId { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public virtual ApplicationUser Driver { get; set; }

        [Range(1,12)]
        public int Slots { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Money { get; set; }

        public int ChatId { get; set; }

        public virtual Chat Chat { get; set; }

        //[InverseProperty("Trips")]
        public virtual ICollection<ApplicationUser> Passengers { get; set; }
    }
}
