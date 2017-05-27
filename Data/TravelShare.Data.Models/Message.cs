using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelShare.Data.Models.Base;

namespace TravelShare.Data.Models
{
    public class Message : BaseModel<int>
    {
        [Required]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        [Required]
        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Content { get; set; }
    }
}
