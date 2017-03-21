namespace TravelShare.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TravelShare.Data.Models.Base;

    public class Trip : BaseModel<int>
    {
        public Trip()
        {
            this.Passenger = new HashSet<ApplicationUser>();
        }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

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

        public virtual ICollection<ApplicationUser> Passenger { get; set; }
    }
}
